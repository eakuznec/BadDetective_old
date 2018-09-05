using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BadDetective
{
    [System.Serializable]
    public struct GameTime
    {
        public float minutes;
        public int hours;
        public int days;
        public int weeks;
        public int months;

        public static GameTime Convert(float time)
        {
            Timeline timeline = Timeline.GetInstantiate();
            int month = (int)(time / timeline.secondsInGameHour / 24 / 7/ 4);
            int week = (int)(time / timeline.secondsInGameHour / 24 / 7 - month*4);
            int day = (int)(time / timeline.secondsInGameHour / 24  - month * 4 * 7 - week*7);
            int hour = (int)(time / timeline.secondsInGameHour - month * 4 * 7 * 24 - week * 7 *24 - day * 24);
            float minute = (time / timeline.secondsInGameHour - month * 4 * 7 * 24 - week * 7 * 24 - day * 24 - hour) * 60;

            return new GameTime()
            {
                minutes = minute,
                hours = hour,
                days = day,
                weeks = week,
                months = month
            };
        }

        public static GameTime Convert(GameTime gameTime)
        {
            float minute = gameTime.minutes;
            int hour = gameTime.hours;
            int day = gameTime.days;
            int week = gameTime.weeks;
            int month = gameTime.months;
            while(minute >= 60)
            {
                hour++;
                minute -= 60;
            }
            while (hour >= 24)
            {
                day++;
                hour -= 24;
            }
            while (day >= 7)
            {
                week++;
                day -= 7;
            }
            while (week >= 4)
            {
                month++;
                week -= 4;
            }

            return new GameTime()
            {
                minutes = minute,
                hours = hour,
                days = day,
                weeks = week,
                months = month
            };
        }

        public static float ConvertToFloat(GameTime gameTime)
        {
            float retVal = 0;
            retVal = gameTime.minutes / 60 + gameTime.hours + gameTime.days * 24 + gameTime.weeks * 24 * 7 + gameTime.months * 24 * 7 * 4;
            return retVal;
        }

        public static GameTime operator +(GameTime time1, GameTime time2)
        {
            float minute = time1.minutes + time2.minutes;
            int hour = time1.hours + time2.hours;
            int day = time1.days + time2.days;
            int week = time1.weeks + time2.weeks;
            int month = time1.months + time2.months;


            return Convert(new GameTime()
            {
                minutes = minute,
                hours = hour,
                days = day,
                weeks = week,
                months = month
            });
        }

        public static bool operator ==(GameTime time1, GameTime time2)
        {
            return ConvertToFloat(time1) == ConvertToFloat(time2);
        }

        public static bool operator !=(GameTime time1, GameTime time2)
        {
            return ConvertToFloat(time1) != ConvertToFloat(time2);
        }

        public static bool operator >(GameTime time1, GameTime time2)
        {
            return ConvertToFloat(time1) > ConvertToFloat(time2);
        }

        public static bool operator <(GameTime time1, GameTime time2)
        {
            return ConvertToFloat(time1) < ConvertToFloat(time2);
        }

        public static bool operator >=(GameTime time1, GameTime time2)
        {
            return ConvertToFloat(time1) >= ConvertToFloat(time2);
        }

        public static bool operator <=(GameTime time1, GameTime time2)
        {
            return ConvertToFloat(time1) <= ConvertToFloat(time2);
        }

        public GameTime GetDayHour()
        {
            return new GameTime()
            {
                minutes = this.minutes,
                hours = this.hours,
                days = 0,
                weeks = 0,
                months = 0
            };
        }

        public bool InRange(GameTime min, GameTime max)
        {
            float minT = min.minutes + min.hours * 60 + min.days * 60 * 24 + min.weeks * 60 * 24 * 7 + min.months * 60 * 24 * 7 * 4;
            float maxT = max.minutes + max.hours * 60 + max.days * 60 * 24 + max.weeks * 60 * 24 * 7 + max.months * 60 * 24 * 7 * 4;
            float T = this.minutes + this.hours * 60 + this.days * 60 * 24 + this.weeks * 60 * 24 * 7 + this.months * 60 * 24 * 7 * 4;
            return (T >= minT) && (T < maxT);
        }

        public bool InDayRange(GameTime min, GameTime max)
        {
            int maxH = max.hours;
            int minH = min.hours;
            if (maxH < minH)
            {
                if (this.hours < maxH)
                {
                    minH -= 24;
                }
                else if (this.hours >= minH)
                {
                    maxH += 24;
                }
            }
            float minT = min.minutes + minH * 60;
            float maxT = max.minutes + maxH * 60;
            float T = this.minutes + this.hours * 60;
            return (T >= minT) && (T < maxT);
        }
    }
}
