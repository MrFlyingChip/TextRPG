using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager
{ 

    static int resultAd = 0;

    public static int ShowRewardedVideo()
    {
        resultAd = 0;
        ShowOptions options = new ShowOptions();
        options.resultCallback = HandleShowResult;

        Advertisement.Show(options);
        return resultAd;
    }

    static void HandleShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            resultAd = 1;
        }
        else if (result == ShowResult.Skipped)
        {
        }
        else if (result == ShowResult.Failed)
        {
        }
        
    }
}
