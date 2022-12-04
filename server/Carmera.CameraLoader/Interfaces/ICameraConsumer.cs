namespace Carmera.CameraLoader.Interfaces;

public interface ICameraConsumer
{
    public (bool success, string error) DoConfiguration();
    public IEnumerable<string> CurrentLocations();
    public IEnumerable<string> ListCameras();
    public (string image, bool success, string error) GetSinglePhoto(string cameraName);
}