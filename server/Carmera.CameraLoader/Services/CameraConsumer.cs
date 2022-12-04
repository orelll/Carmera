using Carmera.CameraLoader.Interfaces;
using Carmera.CameraLoader.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SeeShark;
using SeeShark.Device;
using SeeShark.FFmpeg;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.PixelFormats;

namespace Carmera.CameraLoader.Services;

public class CameraConsumer : ICameraConsumer
{
    private readonly FfmpegOptions _options;
    private bool _configured = false;
    private readonly ILogger<CameraConsumer> _logger;
    private FrameConverter? _converter = null;
    
    public CameraConsumer(IOptions<FfmpegOptions> options, 
        ILogger<CameraConsumer> logger)
    {
        try
        {
            _converter = _converter ?? new FrameConverter(640, 480, PixelFormat.Yuyv422, PixelFormat.Rgb24);
        }
        catch (Exception e)
        {
            // logger.LogError("Zjebało się! { Error }",e.Message);
        }
        _options = options.Value;
        _logger = logger;
    }
    
    public (bool success, string error) DoConfiguration()
    {
        Console.WriteLine($"Current locations: {string.Join(",", _options.Locations)}");
        return Configure();
    }

    public IEnumerable<string> CurrentLocations()
    {
        return _options.Locations;
    }

    public IEnumerable<string> ListCameras()
    {
        var (success, _) = Configure();
        if (!success) return Enumerable.Empty<string>();
        
        using var manager = new CameraManager();
        return manager.Devices.Select(device => $"{device.Name}");

    }

    public (string image, bool success, string error) GetSinglePhoto(string cameraName)
    {
        Configure();
        using (var manager = new CameraManager())
        {
            var device = manager.Devices.FirstOrDefault(cam => cam.Name == cameraName);

            if (device == null)
            {
                _logger.LogInformation("Camera with name {CameraName} not found", cameraName);
                return (string.Empty, false, "Camera not found");
            }
            
            var index = manager.Devices.IndexOf(device);
            _logger.LogInformation("Camera with name {CameraName} found, index: { Index }", cameraName, index);
            using (var camera = manager.GetDevice(1))
            {
                camera.StartCapture();
                Task.Delay(1000);
                var frame = camera.GetFrame();
                camera.StopCapture();

                var imageBase64 = ProcessFrame(frame);
                return (imageBase64.image, imageBase64.success, "empty");
            }
        }
        return (string.Empty, true, "Fake success!");
    }

    private (bool success, string error) Configure()
    {
        try
        {
            if (!_configured)
            {
                FFmpegManager.SetupFFmpeg(
                    FFmpegLogLevel.Verbose,
                    ConsoleColor.DarkCyan,
                    _options.Locations.ToArray()
                );
                _configured = true;
            }
            
            return (true, string.Empty);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return (false, e.Message);
        }
    }

    private (string image, bool success) ProcessFrame(Frame? frame)
    {
        if (frame == null)
        {
            _logger.LogInformation("Frame is empty!");
            return (string.Empty, false);
        }

        string imageBase64 = string.Empty;
        
        _logger.LogInformation("Processing frame...");
        _logger.LogInformation("Basic info: length { Length }, size: { Width }x{ Height }, format: { Format }", frame.RawData.Length, frame.Width, frame.Height, frame.PixelFormat);

        _logger.LogInformation("Creating converter...");
        _converter = _converter ?? new FrameConverter(640, 480, PixelFormat.Yuyv422, PixelFormat.Rgb24);
        var convertedFrame = _converter.Convert(frame);
        _logger.LogInformation("Converted frame length: { Length }, format: { Format }", convertedFrame.RawData.Length, convertedFrame.PixelFormat);

        using (var image = Image.LoadPixelData<Rgb24>(convertedFrame.RawData, convertedFrame.Width, convertedFrame.Height))
        {
            imageBase64 = image.ToBase64String(JpegFormat.Instance);
        }

        return (imageBase64, true);
    }
}