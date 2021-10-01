/*
© Siemens AG, 2017-2018
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

// Added allocation free alternatives
// UoK , 2019, Odysseas Doumas (od79@kent.ac.uk / odydoum@gmail.com)

using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class PoseStampedPublisher : UnityPublisher<MessageTypes.Geometry.PoseStamped>
    {
        public Transform PublishedTransform;
        public string FrameId = "Unity";
        public float offsetvalx = 0;
        public float offsetvaly = 0;
        public float offsetvalz = 0;
        public float offsetvalqx = 0;
        public float offsetvalqy = 0;
        public float offsetvalqz = 0;
        public float offsetValue = 0.001f;
        public float degoff = 20f;
     


        private MessageTypes.Geometry.PoseStamped message;

        protected override void Start()
        {
            base.Start();
            InitializeMessage();
        }

        

        private void InitializeMessage()
        {
            message = new MessageTypes.Geometry.PoseStamped
            {
                header = new MessageTypes.Std.Header()
                {
                    frame_id = FrameId
                }
            };
        }

        private void UpdateMessage() 
        {
            message.header.Update();
            Vector3 offsetVector = new Vector3(offsetvalx, offsetvaly, offsetvalz);
            offsetVector += PublishedTransform.position;
            Debug.Log(offsetVector.x + " "+offsetVector.y+ " " +offsetVector.z +" "  + offsetvalx);
            GetGeometryPoint(offsetVector.Unity2Ros(), message.pose.position);
            Vector3 rot = PublishedTransform.rotation.eulerAngles;
            rot += new Vector3(offsetvalqx, offsetvalqy, offsetvalqz);
            Quaternion quaternion = Quaternion.Euler(rot);
            GetGeometryQuaternion(quaternion.Unity2Ros(), message.pose.orientation);

            Publish(message);
            setzero();
        }

        private void GetGeometryPoint(Vector3 position, MessageTypes.Geometry.Point geometryPoint)
        {   
            geometryPoint.x =  position.x;
            Debug.Log(position.x);
            geometryPoint.y =  position.y;
            geometryPoint.z =  position.z;
        }

        private void GetGeometryQuaternion(Quaternion quaternion, MessageTypes.Geometry.Quaternion geometryQuaternion)
        {
            geometryQuaternion.x =  quaternion.x;
            geometryQuaternion.y =  quaternion.y;
            geometryQuaternion.z =  quaternion.z;
            geometryQuaternion.w = quaternion.w;
        }

        public void AddOffsetx()
        {
           
            offsetvalx += offsetValue;
            // angleText.text = this.Angle.ToString();
        }

        public void SubOffsetx()
        {
            
            offsetvalx -= offsetValue;
            //angleText.text = Angle.ToString();
        }
        public void AddOffsety()
        {
            
            offsetvaly += offsetValue;
            //angleText.text = this.Angle.ToString();
        }

        public void SubOffsety()
        {
            
            offsetvaly -= offsetValue;
            //angleText.text = Angle.ToString();
        }
        public void AddOffsetz()
        {
            
            offsetvalz += offsetValue;
            //angleText.text = this.Angle.ToString();
        }

        public void SubOffsetz()
        {
            
            offsetvalz -= offsetValue;
            //angleText.text = Angle.ToString();
        }
        public void AddOffsetqx()
        {
           
            offsetvalqx += offsetValue;
            //angleText.text = this.Angle.ToString();
        }

        public void SubOffsetqx()
        {
           
            offsetvalqx -= offsetValue;
          //angleText.text = Angle.ToString();
        }
        public void AddOffsetqy()
        {
           
            offsetvalqy += offsetValue;
            //angleText.text = this.Angle.ToString();
        }

        public void SubOffsetqy()
        {
            
            offsetvalqy -= offsetValue;
         // angleText.text = Angle.ToString();
        }
        public void AddOffsetqz()
        {
            
            offsetvalqz += offsetValue;
            //angleText.text = this.Angle.ToString();
        }

        public void SubOffsetqz()
        {
            
            offsetvalqz -= offsetValue;
            //angleText.text = Angle.ToString();
        }

        public void SendPos()
        {
            UpdateMessage();

        }

        public void setzero()
        {
            offsetvalx = 0;
            offsetvaly = 0;
            offsetvalz = 0;
            offsetvalqx = 0;
            offsetvalqy = 0;
            offsetvalqz = 0;
        }

    }
}
