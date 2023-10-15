#import <UIKit/UIKit.h>
#import <AudioToolBox/AudioToolBox.h>

extern "C"
{
    long getFeedbackStyle(const char* style)
    {
        if (strcmp(style, "Heavy") == 0)
            return UIImpactFeedbackStyleHeavy;
        else if (strcmp(style, "Medium") == 0)
            return UIImpactFeedbackStyleMedium;
        else if (strcmp(style, "Light") == 0)
            return UIImpactFeedbackStyleLight;
        else if (strcmp(style, "Rigid") == 0){
            if (@available(iOS 13.0, *))
                return UIImpactFeedbackStyleRigid;
            else return -1;
        }
        else if (strcmp(style, "Soft") == 0){
            if (@available(iOS 13.0, *))
                return UIImpactFeedbackStyleSoft;
            else return -1;
        }
        else return -1;
    }

    void Vibrate(int _n)
    {
        AudioServicesPlaySystemSound(_n);
    }

    void _impactOccurred(const char* style)
    {
        long feedbackStyle = getFeedbackStyle(style);
        if(feedbackStyle == -1)
            return;
        
        UIImpactFeedbackGenerator* generator = [[UIImpactFeedbackGenerator alloc] initWithStyle: (UIImpactFeedbackStyle)feedbackStyle];

        [generator prepare] ;

        [generator impactOccurred] ;
    }

    void _impactOccurredWithIntensity(const char* style, float intensity)
    {
        long feedbackStyle = getFeedbackStyle(style);
        if(feedbackStyle == -1)
            return;
        
        UIImpactFeedbackGenerator* generator = [[UIImpactFeedbackGenerator alloc] initWithStyle: (UIImpactFeedbackStyle)feedbackStyle];

        [generator prepare] ;

        [generator impactOccurredWithIntensity : intensity] ;
    }

    void _notificationOccurred(const char* style)
    {
        UINotificationFeedbackType feedbackStyle;
        if (strcmp(style, "Error") == 0)
            feedbackStyle = UINotificationFeedbackTypeError;
        else if (strcmp(style, "Success") == 0)
            feedbackStyle = UINotificationFeedbackTypeSuccess;
        else if (strcmp(style, "Warning") == 0)
            feedbackStyle = UINotificationFeedbackTypeWarning;
        else return;

        UINotificationFeedbackGenerator* generator = [[UINotificationFeedbackGenerator alloc] init];

        [generator prepare] ;

        [generator notificationOccurred : feedbackStyle] ;
    }
}
