# Video Splash Screen Setup

## Video Configuration

The splash screen now uses an embedded video (RainDance.mp4) that plays when the application starts. Key features:

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