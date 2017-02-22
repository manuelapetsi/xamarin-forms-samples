﻿using System;
using Xamarin.Forms;
using TouchTracking;

namespace TouchTrackingEffectDemos
{
    class DraggableBoxView : BoxView
    {
        bool isBeingDragged;
        long touchId;
        Point pressPoint;

        public DraggableBoxView()
        {
            TouchEffect touchEffect = new TouchEffect
            {
                Capture = true
            };
            touchEffect.TouchAction += OnTouchAction;
            Effects.Add(touchEffect);
        }

        void OnTouchAction(object sender, TouchActionEventArgs args)
        {
            switch (args.Type)
            {
                case TouchActionType.Pressed:
                    if (!isBeingDragged && args.IsInContact)
                    {
                        isBeingDragged = true;
                        touchId = args.Id;
                        pressPoint = args.Location;
                    }
                    break;

                case TouchActionType.Moved:
                    if (isBeingDragged && touchId == args.Id)
                    {
                        TranslationX += args.Location.X - pressPoint.X;
                        TranslationY += args.Location.Y - pressPoint.Y;
                    }
                    break;

                case TouchActionType.Released:
                    if (isBeingDragged && touchId == args.Id)
                    {
                        isBeingDragged = false;
                    }
                    break;
            }
        }
    }
}
