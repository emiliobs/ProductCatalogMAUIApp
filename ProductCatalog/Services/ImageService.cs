using System;
using System.Collections.Generic;
using System.Text;

namespace ProductCatalog.Services;

/// <summary>
/// Service for handling image operations
/// Takes photos, selects from gallery, and manages image files
/// </summary>
public class ImageService
{
    private readonly string _imagesFolder;

    public ImageService()
    {
        // Create images folder in app data directory
        _imagesFolder = Path.Combine(FileSystem.AppDataDirectory, "images");

        // Create folder if it doesn't exist
        if (!Directory.Exists(_imagesFolder))
        {
            Directory.CreateDirectory(_imagesFolder);
        }
    }

    /// <summary>
    /// Take a photo using device camera
    /// </summary>
    /// <returns>Path to saved image or null if cancelled/failed</returns>
    public async Task<string?> TakePhotoAsync()
    {
        try
        {
            // Check if camera is supported
            if (!MediaPicker.Default.IsCaptureSupported)
            {
                return null;
            }

            // Open camera and take photo
            var photo = await MediaPicker.Default.CapturePhotoAsync();

            if (photo == null)
                return null;

            // Save and return path
            return await SaveImageAsync(photo);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error taking photo: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Select a photo from device gallery
    /// </summary>
    /// <returns>Path to saved image or null if cancelled/failed</returns>
    public async Task<string?> SelectPhotoAsync()
    {
        try
        {
            // Open gallery picker
            var photo = await MediaPicker.Default.PickPhotoAsync();

            if (photo == null)
                return null;

            // Save and return path
            return await SaveImageAsync(photo);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error selecting photo: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Save image file to local storage
    /// </summary>
    /// <param name="photo">File result from MediaPicker</param>
    /// <returns>Path to saved image</returns>
    private async Task<string> SaveImageAsync(FileResult photo)
    {
        // Generate unique filename using GUID
        var fileName = $"{Guid.NewGuid()}.jpg";
        var destinationPath = Path.Combine(_imagesFolder, fileName);

        // Copy image to our folder
        using var sourceStream = await photo.OpenReadAsync();
        using var destinationStream = File.Create(destinationPath);
        await sourceStream.CopyToAsync(destinationStream);

        return destinationPath;
    }

    /// <summary>
    /// Delete an image file
    /// </summary>
    /// <param name="imagePath">Path to image to delete</param>
    public void DeleteImage(string? imagePath)
    {
        try
        {
            if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error deleting image: {ex.Message}");
        }
    }
}