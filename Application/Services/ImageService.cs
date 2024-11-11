using Application.IService;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Domain.Entities;
using Infrastructure.IRepositories;
using Infrastructure.ServiceResponse;
using Infrastructure.ViewModels.ImageDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Application.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageRepo _imageRepo;
        private readonly IMapper _mapper;
        private readonly Cloudinary _cloudinary;

        public ImageService(IImageRepo imageRepo, IMapper mapper, Cloudinary cloudinary)
        {
            _imageRepo = imageRepo ?? throw new ArgumentNullException(nameof(imageRepo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _cloudinary = cloudinary ?? throw new ArgumentNullException(nameof(cloudinary));
        }

        public async Task<ServiceResponse<IEnumerable<ImageDTO>>> GetAllImageInfors()
        {
            var response = new ServiceResponse<IEnumerable<ImageDTO>>();

            try
            {
                var images = await _imageRepo.GetAllImageInfors();
                var imageDTOs = _mapper.Map<IEnumerable<ImageDTO>>(images);

                response.Data = imageDTOs;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Failed to retrieve image infors: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<IEnumerable<ImageDTO>>> GetImageInforById(int entityId, string entityType)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<ImageDTO>>();

            try
            {
                var images = await _imageRepo.GetImagesByEntityIdAndType(entityId, entityType);

                if (images == null || !images.Any())
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "No images found for the specified entity.";
                }
                else
                {
                    var imageDTOs = _mapper.Map<IEnumerable<ImageDTO>>(images);
                    serviceResponse.Data = imageDTOs;
                    serviceResponse.Success = true;
                    serviceResponse.Message = "Images retrieved successfully.";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"An error occurred: {ex.Message}";
            }

            return serviceResponse;
        }


        public async Task<ServiceResponse<string>> DeleteImage(int id)
        {
            var serviceResponse = new ServiceResponse<string>();

            try
            {
                await _imageRepo.DeleteImage(id);
                serviceResponse.Success = true;
                serviceResponse.Message = "Image deleted successfully";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Failed to delete image: " + ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<ImageDTO>> UploadImage(IFormFile file, int entityId, string entityType)
        {
            var serviceResponse = new ServiceResponse<ImageDTO>();

            try
            {
                if (file.Length > 0)
                {
                    // Set up Cloudinary upload parameters
                    using (var stream = file.OpenReadStream())
                    {
                        var uploadParams = new ImageUploadParams
                        {
                            File = new FileDescription(file.FileName, stream),
                            Transformation = new Transformation().Crop("fill").Gravity("face") // Adjust transformation as needed
                        };

                        // Upload to Cloudinary
                        var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                        if (uploadResult?.Url != null)
                        {
                            // Check if there are existing images for the entity
                            bool hasExistingImages = await _imageRepo.GetImageInforById(entityId);

                            // Create an EntityImage object to store in the database
                            var entityImage = new EntityImage
                            {
                                EntityId = entityId,                  
                                EntityType = entityType,           
                                ImageUrl = uploadResult.Url.ToString(),
                                UploadAt = DateTime.UtcNow,
                                IsPrimary = !hasExistingImages        
                            };

                            // Save the image entity in the database and get the saved entity
                            var savedImage = await _imageRepo.AddImage(entityImage);

                            // Map the saved entity to DTO for response
                            var imageDTO = _mapper.Map<ImageDTO>(savedImage);
                            serviceResponse.Data = imageDTO;
                            serviceResponse.Success = true;
                            serviceResponse.Message = "Image uploaded successfully";
                        }
                        else
                        {
                            serviceResponse.Success = false;
                            serviceResponse.Message = "Image upload failed";
                        }
                    }
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Invalid file";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Failed to upload image: " + ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<string>>> UploadImageFromUrl(ImageUploadRequest uploadRequest)
        {
            var response = new ServiceResponse<List<string>> { Data = new List<string>() };
            try
            {
                // Check if there are existing images for this entity to set primary status
                bool hasExistingImages = await _imageRepo.GetImageInforById(uploadRequest.EntityId);

                // Loop through each image URL in the request
                foreach (var imageUrl in uploadRequest.ImageUrls)
                {
                    // Create a new image entity for each image URL
                    var entityImage = new EntityImage
                    {
                        EntityId = uploadRequest.EntityId,
                        EntityType = uploadRequest.EntityType,
                        ImageUrl = imageUrl,
                        UploadAt = DateTime.UtcNow,
                        IsPrimary = !hasExistingImages // Set primary if this is the first image
                    };

                    // Save the image entity to the database
                    var savedImage = await _imageRepo.AddImage(entityImage);

                    // Add the saved image URL to the response list
                    response.Data.Add(savedImage.ImageUrl);

                    // After the first image is saved, set hasExistingImages to true to avoid multiple primaries
                    hasExistingImages = true;
                }

                response.Success = true;
                response.Message = "Images uploaded successfully!";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
