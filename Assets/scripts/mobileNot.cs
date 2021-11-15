using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;

public class mobileNot : MonoBehaviour
{
    public static string reminderChannelId = "channel_id";

    void Start()
    {
        AndroidNotificationCenter.CancelAllDisplayedNotifications();
        AndroidNotificationCenter.CancelAllScheduledNotifications();

        var channel = InitChannel(reminderChannelId);

        AndroidNotification notification = new AndroidNotification() {
            Title = "Don't forget to train today!",
            Text = "",
            RepeatInterval = new TimeSpan(1, 0, 0, 0),
            SmallIcon = "custom_small",
            LargeIcon = "custom_large",
            ShouldAutoCancel = true,
        };
        SendRepeat(notification, reminderChannelId, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0));
        SendRepeat(notification, reminderChannelId, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 13, 0, 0));
        SendRepeat(notification, reminderChannelId, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 18, 0, 0));
        
        AndroidNotificationCenter.NotificationReceivedCallback receivedNotificationHandler =
            delegate (AndroidNotificationIntentData data) {
                AndroidNotificationCenter.CancelAllDisplayedNotifications();
            };
        AndroidNotificationCenter.OnNotificationReceived += receivedNotificationHandler;
    }

    AndroidNotificationChannel InitChannel(string id) {
        var channel = new AndroidNotificationChannel() {
            Id = id,
            Name = "Reminder Channel",
            Importance = Importance.Default,
            Description = "Regular reminder notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);
        return channel;
    }

    void SendRepeat(AndroidNotification notification, string channelId, DateTime fireTime) {
        if (DateTime.Now.Hour < fireTime.Hour) {
            notification.FireTime = fireTime;
        }
        else {
            notification.FireTime = fireTime.Add(notification.RepeatInterval.GetValueOrDefault());
        }
        AndroidNotificationCenter.SendNotification(notification, reminderChannelId);
    }
}
