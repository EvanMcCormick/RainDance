# Video Splash Screen Setup

## Required Setup Steps

To get the video splash screen working in Visual Studio, you need to:

1. Install the Windows Media Player ActiveX control:
   ```
   Right-click on Toolbox > Choose Items... > COM Components tab
   Check "Windows Media Player" > OK
   ```

2. Fix the references:
   ```
   Right-click on References in Solution Explorer
   Add > COM > Windows Media Player
   ```

3. Place video file:
   ```
   Copy your RainDance.mp4 file to the Resources folder in your project
   ```

## Alternative Method (If first method fails)

If you're still having issues:

1. Install the WMPLib NuGet package:
   ```
   Right-click on the project > Manage NuGet Packages
   Search for "WMPLib" > Install the package
   ```

2. Fix the using statements:
   ```csharp
   // At the top of Form1.cs
   using WMPLib;
   using AxWMPLib;
   ```

## Video Configuration

The splash screen plays a video (RainDance.mp4) when the application starts:

- Video plays without any media controls (fullscreen mode)
- Right-click context menu is disabled
- Video is stretched to fill the entire form
- Duration set to 30 seconds to match the video length
- When video ends OR when the 30-second timer expires, the main UI appears
- Press the SPACE BAR at any time to skip the video and go directly to the main UI

## Video File Location

The RainDance.mp4 video should be placed in:
- Project folder: /Resources/RainDance.mp4

The project is configured to copy this file to the output directory when building, so the video will be available at runtime.

## Implementation Details

- Uses the Windows Media Player ActiveX control (AxWindowsMediaPlayer)
- Automatically plays the video when the form loads
- Hides all other UI elements until the video finishes
- All playback controls are hidden for a clean look

## Customization

- To change the video duration, modify the `Interval` property of the `splashTimer` in Form1.cs
- To use a different video file, update the `videoPath` variable in the `SetupSplashScreen` method

## Troubleshooting

If you encounter issues:
- Check that the RainDance.mp4 file exists in the Resources folder
- Ensure the Windows Media Player control is properly installed
- Make sure the video format is supported by Windows Media Player
- If the video doesn't automatically play, check the console for warnings