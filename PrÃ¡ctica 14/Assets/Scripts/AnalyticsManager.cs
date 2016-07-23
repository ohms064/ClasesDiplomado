using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Analytics;


public class AnalyticsManager : MonoBehaviour {
    public static AnalyticsManager instance;
    public GoogleAnalyticsV3 googleAnalytics;

	// Use this for initialization
	void Awake () {
        instance = this;
	}

    public void ScreenEvent(string screenName) {
        Analytics.CustomEvent(screenName);
        googleAnalytics.LogScreen(new AppViewHitBuilder().SetScreenName(screenName));
    }

    public void RegisterEvent(string category, string action, string label, long eventValue) {
        Analytics.CustomEvent(action, new Dictionary<string, object> {
            {"category", category },
            { "label", label},
            {"Game time", eventValue }
        }
        );
        googleAnalytics.LogEvent(new EventHitBuilder()
            .SetEventCategory(category)
            .SetEventAction(action)
            .SetEventLabel(label)
            .SetEventValue(eventValue));
    }
}
