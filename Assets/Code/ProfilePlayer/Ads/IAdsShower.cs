using System;

namespace Tools
{
    internal interface IAdsShower
    {
        void ShowInterstitial();
        void ShowVideo(Action successShow);
    }
}

