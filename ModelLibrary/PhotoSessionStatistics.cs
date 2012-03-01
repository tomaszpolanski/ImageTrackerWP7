using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace ModelLibrary
{
    public class PhotoSessionStatistics
    {
        public static double AvarageWeight(PhotoSessions photoSessions)
        {
            if (photoSessions.PhotoSessionCollection.Count == 0)
            {
                return 0;
            }
            double sum = 0;
            int emptyWeightSessions = 0;
            foreach (PhotoSession session in photoSessions.PhotoSessionCollection)
            {
                sum += session.Weight;
                if (session.Weight == 0)
                {
                    emptyWeightSessions++;
                }
            }

            int sessionsWithWeight = photoSessions.PhotoSessionCollection.Count - emptyWeightSessions;
            if (sessionsWithWeight > 0)
            {
                return sum / sessionsWithWeight;
            }
            return 0;
        }

        public static double CurrentWeightLoss(PhotoSessions photoSessions)
        {
            if (photoSessions.PhotoSessionCollection.Count == 0)
            {
                return 0;
            }
            return photoSessions.PhotoSessionCollection[photoSessions.PhotoSessionCollection.Count - 1].Weight - photoSessions.PhotoSessionCollection[0].Weight;
        }

        public static double CurrentBMI(PhotoSessions photoSessions, double height)
        {
            if (photoSessions.PhotoSessionCollection.Count == 0)
            {
                return 0;
            }
            return photoSessions.PhotoSessionCollection[0].Weight / (double)(Math.Pow(height / 100, 2));
        }
    }
}
