using GoogleARCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageScanner : MonoBehaviour {

    public GameObject prefab;
    public bool spawned;
    private List<AugmentedImage> m_TempAugmentedImages = new List<AugmentedImage>();

    private void Update()
    {
        // Get updated augmented images for this frame.
        Session.GetTrackables<AugmentedImage>(m_TempAugmentedImages, TrackableQueryFilter.Updated);
        // Create visualizers and anchors for updated augmented images that are tracking and do not previously
        // have a visualizer. Remove visualizers for stopped images.
        foreach (var image in m_TempAugmentedImages)
        {
            Debug.Log(image.TrackingState);
            if (image.TrackingState == TrackingState.Tracking && !spawned)
            {
                spawned = true;

                // Create an anchor to ensure that ARCore keeps tracking this augmented image.
                Anchor anchor = image.CreateAnchor(image.CenterPose);
                Instantiate(prefab, anchor.transform);
            }
        }
    }
}
