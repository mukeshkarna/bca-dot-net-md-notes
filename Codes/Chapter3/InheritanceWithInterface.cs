using System;
using System.Collections.Generic;

// First interface for playable devices
public interface IPlayable
{
  string MediaType { get; }
  bool IsPlaying { get; }

  void Play();
  void Pause();
  void Stop();
}

// Second interface for recordable devices
public interface IRecordable
{
  bool IsRecording { get; }
  string RecordingFormat { get; }

  void Record();
  void StopRecording();
}

// Abstract base class for all media devices
public abstract class MediaDevice
{
  public string Brand { get; set; }
  public string Model { get; set; }
  public int Volume { get; private set; }
  public bool IsPoweredOn { get; private set; }

  protected MediaDevice(string brand, string model)
  {
    Brand = brand;
    Model = model;
    Volume = 50; // Default volume
    IsPoweredOn = false;
  }

  public virtual void PowerOn()
  {
    IsPoweredOn = true;
    Console.WriteLine($"{Brand} {Model} is now powered on.");
  }

  public virtual void PowerOff()
  {
    IsPoweredOn = false;
    Console.WriteLine($"{Brand} {Model} is now powered off.");
  }

  public void SetVolume(int volume)
  {
    if (volume < 0 || volume > 100)
    {
      throw new ArgumentOutOfRangeException(nameof(volume), "Volume must be between 0 and 100.");
    }
    Volume = volume;
    Console.WriteLine($"Volume set to {Volume}");
  }

  public abstract string GetDeviceInfo();
}

// CD Player - implements only IPlayable
public class CDPlayer : MediaDevice, IPlayable
{
  private bool _isPlaying;
  private string _currentTrack;

  public string MediaType => "CD";
  public bool IsPlaying => _isPlaying;

  public CDPlayer(string brand, string model) : base(brand, model)
  {
    _isPlaying = false;
    _currentTrack = null;
  }

  public void Play()
  {
    if (!IsPoweredOn)
    {
      Console.WriteLine("Device is powered off. Please turn it on first.");
      return;
    }

    _isPlaying = true;
    _currentTrack = "Track 1"; // Simulated track
    Console.WriteLine($"{Brand} {Model} is now playing {_currentTrack} from {MediaType}");
  }

  public void Pause()
  {
    if (_isPlaying)
    {
      _isPlaying = false;
      Console.WriteLine($"{Brand} {Model} is paused.");
    }
    else
    {
      Console.WriteLine("Nothing is playing.");
    }
  }

  public void Stop()
  {
    if (_isPlaying)
    {
      _isPlaying = false;
      _currentTrack = null;
      Console.WriteLine($"{Brand} {Model} has stopped playback.");
    }
    else
    {
      Console.WriteLine("Nothing is playing.");
    }
  }

  public void Eject()
  {
    Stop();
    Console.WriteLine("CD ejected.");
  }

  public override string GetDeviceInfo()
  {
    return $"CD Player: {Brand} {Model}, Volume: {Volume}, Playing: {IsPlaying}";
  }
}

// DVD Player - implements both IPlayable and IRecordable
public class DVDPlayer : MediaDevice, IPlayable, IRecordable
{
  private bool _isPlaying;
  private bool _isRecording;
  private string _currentMedia;

  public string MediaType => "DVD";
  public bool IsPlaying => _isPlaying;
  public bool IsRecording => _isRecording;
  public string RecordingFormat => "DVD-R";

  public DVDPlayer(string brand, string model) : base(brand, model)
  {
    _isPlaying = false;
    _isRecording = false;
    _currentMedia = null;
  }

  public void Play()
  {
    if (!IsPoweredOn)
    {
      Console.WriteLine("Device is powered off. Please turn it on first.");
      return;
    }

    if (_isRecording)
    {
      Console.WriteLine("Cannot play while recording.");
      return;
    }

    _isPlaying = true;
    _currentMedia = "Movie Title"; // Simulated media
    Console.WriteLine($"{Brand} {Model} is now playing {_currentMedia} from {MediaType}");
  }

  public void Pause()
  {
    if (_isPlaying)
    {
      _isPlaying = false;
      Console.WriteLine($"{Brand} {Model} is paused.");
    }
    else
    {
      Console.WriteLine("Nothing is playing.");
    }
  }

  public void Stop()
  {
    if (_isPlaying)
    {
      _isPlaying = false;
      _currentMedia = null;
      Console.WriteLine($"{Brand} {Model} has stopped playback.");
    }
    else
    {
      Console.WriteLine("Nothing is playing.");
    }
  }

  public void Record()
  {
    if (!IsPoweredOn)
    {
      Console.WriteLine("Device is powered off. Please turn it on first.");
      return;
    }

    if (_isPlaying)
    {
      Console.WriteLine("Cannot record while playing.");
      return;
    }

    _isRecording = true;
    Console.WriteLine($"{Brand} {Model} is now recording in {RecordingFormat} format.");
  }

  public void StopRecording()
  {
    if (_isRecording)
    {
      _isRecording = false;
      Console.WriteLine($"{Brand} {Model} has stopped recording.");
    }
    else
    {
      Console.WriteLine("Not currently recording.");
    }
  }

  public override string GetDeviceInfo()
  {
    return $"DVD Player: {Brand} {Model}, Volume: {Volume}, Playing: {IsPlaying}, Recording: {IsRecording}";
  }
}

// Additional device - Blu-ray Player (extends functionality)
public class BluRayPlayer : MediaDevice, IPlayable, IRecordable
{
  private bool _isPlaying;
  private bool _isRecording;
  private string _currentMedia;
  private bool _is3DEnabled;

  public string MediaType => "Blu-ray";
  public bool IsPlaying => _isPlaying;
  public bool IsRecording => _isRecording;
  public string RecordingFormat => "BD-R";
  public bool Is3DEnabled => _is3DEnabled;

  public BluRayPlayer(string brand, string model) : base(brand, model)
  {
    _isPlaying = false;
    _isRecording = false;
    _currentMedia = null;
    _is3DEnabled = false;
  }

  public void Play()
  {
    if (!IsPoweredOn)
    {
      Console.WriteLine("Device is powered off. Please turn it on first.");
      return;
    }

    if (_isRecording)
    {
      Console.WriteLine("Cannot play while recording.");
      return;
    }

    _isPlaying = true;
    _currentMedia = "4K Movie"; // Simulated media
    Console.WriteLine($"{Brand} {Model} is now playing {_currentMedia} from {MediaType}");
    if (_is3DEnabled)
    {
      Console.WriteLine("3D mode is enabled.");
    }
  }

  public void Pause()
  {
    if (_isPlaying)
    {
      _isPlaying = false;
      Console.WriteLine($"{Brand} {Model} is paused.");
    }
    else
    {
      Console.WriteLine("Nothing is playing.");
    }
  }

  public void Stop()
  {
    if (_isPlaying)
    {
      _isPlaying = false;
      _currentMedia = null;
      Console.WriteLine($"{Brand} {Model} has stopped playback.");
    }
    else
    {
      Console.WriteLine("Nothing is playing.");
    }
  }

  public void Record()
  {
    if (!IsPoweredOn)
    {
      Console.WriteLine("Device is powered off. Please turn it on first.");
      return;
    }

    if (_isPlaying)
    {
      Console.WriteLine("Cannot record while playing.");
      return;
    }

    _isRecording = true;
    Console.WriteLine($"{Brand} {Model} is now recording in {RecordingFormat} format (High Definition).");
  }

  public void StopRecording()
  {
    if (_isRecording)
    {
      _isRecording = false;
      Console.WriteLine($"{Brand} {Model} has stopped recording.");
    }
    else
    {
      Console.WriteLine("Not currently recording.");
    }
  }

  public void Toggle3D()
  {
    _is3DEnabled = !_is3DEnabled;
    Console.WriteLine($"3D mode is now {(_is3DEnabled ? "enabled" : "disabled")}.");
  }

  public override string GetDeviceInfo()
  {
    return $"Blu-ray Player: {Brand} {Model}, Volume: {Volume}, Playing: {IsPlaying}, Recording: {IsRecording}, 3D: {Is3DEnabled}";
  }
}

// Media Player Manager to demonstrate polymorphism
public class MediaPlayerManager
{
  private List<IPlayable> playableDevices;
  private List<IRecordable> recordableDevices;

  public MediaPlayerManager()
  {
    playableDevices = new List<IPlayable>();
    recordableDevices = new List<IRecordable>();
  }

  public void AddDevice(MediaDevice device)
  {
    if (device is IPlayable playable)
    {
      playableDevices.Add(playable);
    }

    if (device is IRecordable recordable)
    {
      recordableDevices.Add(recordable);
    }
  }

  public void PlayAllDevices()
  {
    Console.WriteLine("\n=== Playing all playable devices ===");
    foreach (IPlayable device in playableDevices)
    {
      device.Play();
    }
  }

  public void StopAllDevices()
  {
    Console.WriteLine("\n=== Stopping all playable devices ===");
    foreach (IPlayable device in playableDevices)
    {
      device.Stop();
    }
  }

  public void StartRecordingOnAll()
  {
    Console.WriteLine("\n=== Starting recording on all recordable devices ===");
    foreach (IRecordable device in recordableDevices)
    {
      device.Record();
    }
  }

  public void ShowAllDeviceInfo()
  {
    Console.WriteLine("\n=== All Device Information ===");
    HashSet<MediaDevice> uniqueDevices = new HashSet<MediaDevice>();

    foreach (IPlayable device in playableDevices)
    {
      if (device is MediaDevice mediaDevice)
      {
        uniqueDevices.Add(mediaDevice);
      }
    }

    foreach (MediaDevice device in uniqueDevices)
    {
      Console.WriteLine(device.GetDeviceInfo());
    }
  }
}

// Usage example demonstrating polymorphism
class Program
{
  static void Main()
  {
    // Create various media devices
    CDPlayer cdPlayer = new CDPlayer("Sony", "CD-100");
    DVDPlayer dvdPlayer = new DVDPlayer("Samsung", "DVD-200");
    BluRayPlayer bluRayPlayer = new BluRayPlayer("LG", "BR-300");

    // Power on devices
    cdPlayer.PowerOn();
    dvdPlayer.PowerOn();
    bluRayPlayer.PowerOn();

    // Create manager and add devices
    MediaPlayerManager manager = new MediaPlayerManager();
    manager.AddDevice(cdPlayer);
    manager.AddDevice(dvdPlayer);
    manager.AddDevice(bluRayPlayer);

    // Demonstrate polymorphism with IPlayable collection
    Console.WriteLine("\n=== Demonstrating Polymorphism with IPlayable ===");
    List<IPlayable> playableDevices = new List<IPlayable> { cdPlayer, dvdPlayer, bluRayPlayer };

    foreach (IPlayable device in playableDevices)
    {
      Console.WriteLine($"\nTesting {device.MediaType} player:");
      device.Play();
      device.Pause();
      device.Play();
    }

    // Demonstrate polymorphism with IRecordable collection
    Console.WriteLine("\n=== Demonstrating Polymorphism with IRecordable ===");
    List<IRecordable> recordableDevices = new List<IRecordable> { dvdPlayer, bluRayPlayer };

    foreach (IRecordable device in recordableDevices)
    {
      Console.WriteLine($"\nTesting recording on {device.RecordingFormat} format:");
      device.Record();
    }

    // Use manager to control all devices
    manager.StopAllDevices();
    manager.StartRecordingOnAll();

    // Test specific functionality
    Console.WriteLine("\n=== Testing Specific Device Features ===");
    cdPlayer.Eject();
    bluRayPlayer.Toggle3D();

    // Show all device information
    manager.ShowAllDeviceInfo();

    // Test interface checking
    Console.WriteLine("\n=== Testing Interface Implementations ===");
    TestDeviceCapabilities(cdPlayer);
    TestDeviceCapabilities(dvdPlayer);
    TestDeviceCapabilities(bluRayPlayer);

    // Volume control demonstration
    Console.WriteLine("\n=== Testing Volume Control ===");
    dvdPlayer.SetVolume(75);
    bluRayPlayer.SetVolume(60);

    // Power off all devices
    Console.WriteLine("\n=== Powering Off All Devices ===");
    cdPlayer.PowerOff();
    dvdPlayer.PowerOff();
    bluRayPlayer.PowerOff();
  }

  // Helper method to test device capabilities
  static void TestDeviceCapabilities(MediaDevice device)
  {
    Console.WriteLine($"\n{device.Brand} {device.Model} capabilities:");

    if (device is IPlayable)
    {
      Console.WriteLine("- Can play media");
    }

    if (device is IRecordable recordable)
    {
      Console.WriteLine($"- Can record media in {recordable.RecordingFormat} format");
    }

    Console.WriteLine($"- Device info: {device.GetDeviceInfo()}");
  }
}