# Vibration for Android and iOS (Unity)

[Introduction](#introduction) | [Sample App](#sample-app) | [Installation](#installation) | [Code Overview](#code-overview)

## Introduction
I made this "vibration utility for Unity" to ease your app development.<br/>
When I developed the vibration with Unity at work, I spent a lot of time testing the vibration, and getting feedbacks. <br/>
Here are the sample codes and plugin(iOS) ready for you! <br/>
Feel free to use my script and sample app!

All of the scripts are written per official AOS/iOS documents.<br/>
Please don't forget to read the document before use.<br/>
<b>Refer to the "Reference" section for details.</b><br/>

All of the Android predefined vibration types are made by trial and errors. <br/>
Tried my best to make AOS vibration as similar as iOS predefined vibration types. <br/>
Try out my sample app to customize your own vibration conveniently.

Any feedbacks are always welcome.
Submit issues if you have any. I will reply to your issues ASAP.
(Enhancement, bugs, advice, etc)

### Sample App
Google Play : [Vibration Tester](https://play.google.com/store/apps/details?id=com.IsDororok.VibrationTester)

## Installation
- Copy all files in ./Assets/Scripts/
  - VibrationInstance.cs
  - VibrationAndroid.cs
  - VibrationEditor.cs
  - VibrationIOS.cs
  - VibrationUtil.cs

- Android :
  - Add `<uses-permission android:name = "android.permission.VIBRATE"/>` in your AndroidManifest.xml
- iOS :
  * Copy plugin file in ./Assets/Plugins
    * VibrationPlugin.mm

## Requirements
- Android
  - API Level >= 26
  - Permssion : `<uses-permission android:name = "android.permission.VIBRATE"/>`
- IOS
  - OS Version : >= 10.0

## Tested On
- Galaxy Note 20 (AOS 13)
- Galaxy A52s (AOS 13)
- Iphone 12 mini (iOS 16 & 17)

## Code Overview
### VibrationUtil
#### Vibrate
```
public abstract void Vibrate(VibrationType vibrationType);
```
###### Details
Vibrates using pre-defined types.
See Vibrationtype 

#### VibrationType
```
enum VibrationType
```
###### Details
Default = 1352,
Peek = 1519,
Pop = 1520,
Nope = 1521,
Heavy,
Medium,
Light,
Rigid,
Soft,
Error,
Success,
Warning

#### AmplitudeType
```
public class AmplitudeType {}
```
###### Details
Vibration Amplitude Types <br/>
These values are adjusted by trial and errors on Samsung Galaxy.<br/>

<br/>

#### LengthType
```
public class LengthType {}
```
(Customized) Vibration length (duration) types <br/>
These values are adjusted by trial and error on Samsung Galaxy.<br/>

---

### VibrationAndroid
```
public class VibrationAndroid : VibrationInstance { }
```
###### Details
Implements vibration method for AOS </br>

</br>

#### IsVibrationAvailable
```
protected override bool IsVibrationAvailable();
```
###### Details
Checks if the Android device supports the vibration API.
Checks 2 functions : HasVibrator && API Level >= 26
###### Returns
- True : Available
- False : Not Available

---

### VibrationIOS
```
public class VibrationIOS : VibrationInstance { }
```
###### Details
Implements vibration method for iOS
</br></br>

#### IsVibrationAvailable
```
protected override bool IsVibrationAvailable();
```
###### Details
Does not check availability for iOS for now. Always return true.
###### Returns
- True : Available

---

### References
- https://developer.apple.com/documentation/uikit/uinotificationfeedbackgenerator
- https://developer.android.com/reference/android/os/Vibrator#hasVibrator()
- https://developer.android.com/reference/android/os/VibrationEffect
