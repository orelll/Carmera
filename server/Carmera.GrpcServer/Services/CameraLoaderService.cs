using Carmera.CameraLoader.Interfaces;
using Grpc;
using Grpc.Core;

namespace Carmera.GrpcServer.Services;

public class CameraLoaderService : CarmeraLoader.CarmeraLoaderBase
{
    private readonly ILogger<CameraLoaderService> _logger;
    private readonly ICameraConsumer _cameraConsumer;

    public CameraLoaderService(ICameraConsumer cameraConsumer,
        ILogger<CameraLoaderService> logger)
    {
        _cameraConsumer = cameraConsumer;
        _logger = logger;
    }

    public override Task<BooleanReply> HealthCheck(EmptyRequest request, ServerCallContext context)
    {
        return Task.FromResult(new BooleanReply
        {
            Value = true
        });
    }

    public override Task<CurrentLocationsReply> CurrentLocations(EmptyRequest request, ServerCallContext context)
    {
        var response = new CurrentLocationsReply();
        response.Locations.AddRange(_cameraConsumer.CurrentLocations());
        
        return Task.FromResult(response);
    }

    public override Task<ListCamerasReply> ListCameras(EmptyRequest request, ServerCallContext context)
    {
        var cameras = _cameraConsumer.ListCameras();
        var response = new ListCamerasReply();
        response.Cameras.AddRange(cameras);
        
        return Task.FromResult(response);
    }

    public override Task<BooleanReply> CheckConfiguration(EmptyRequest request, ServerCallContext context)
    {
        var configurationResult = _cameraConsumer.DoConfiguration();
        var response = new BooleanReply
        {
            Value = configurationResult.success,
            Message = configurationResult.error
        };
        
        return Task.FromResult(response);
    }

    public override Task<GetPhotoReply> GetPhoto(GetPhotoRequest request, ServerCallContext context)
    {
        var photoData = _cameraConsumer.GetSinglePhoto(request.CameraName);
        var reply = new GetPhotoReply();
        reply.Data = photoData.image;
        reply.Success = photoData.success;
        
        return Task.FromResult(reply);
    }
}