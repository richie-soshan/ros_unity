/*
© Siemens AG, 2020
Author: Dr. Martin Bischoff (martin.bischoff@siemens.com)

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at
<http://www.apache.org/licenses/LICENSE-2.0>.
Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using System;
using UnityEngine;
using UnityEngine.UI;

public class HingeJointAngleCalculator : MonoBehaviour
{
    private HingeJoint _hingeJoint;
    private Vector3 axis;
    private Vector3 vectorInRotationPlane;
    private Quaternion initialRotationInverse;

    private float offsetAngle =0;
    public float finalOffset;
    public TMPro.TMP_Text angleText;
    public float Angle
    {
        get
        {
            Quaternion actualRelativeRotation = initialRotationInverse * transform.localRotation;
            return finalOffset+Vector3.SignedAngle(vectorInRotationPlane, actualRelativeRotation * vectorInRotationPlane, axis);
        }
    }

    private void Awake()
    {
        _hingeJoint = GetComponent<HingeJoint>();
        axis = _hingeJoint.axis;
        vectorInRotationPlane = Vector3.right;
        Vector3.OrthoNormalize(ref axis, ref vectorInRotationPlane);
        initialRotationInverse = Quaternion.Inverse(transform.localRotation);
    }

    public void AddOffset()
    {
        float offsetValue = 0.5f;
        finalOffset += offsetValue;
        angleText.text = this.Angle.ToString();
    }
    
    public void SubOffset()
    {
        float offsetValue = 0.5f;
        finalOffset -= offsetValue;
        angleText.text = Angle.ToString();
    }

    public void setOffset()
    {
        finalOffset = 0;
        angleText.text = Angle.ToString();
    }
}