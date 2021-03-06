  
/*
© Siemens AG, 2017-2019
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

using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class JointStatePublisher : UnityPublisher<MessageTypes.Sensor.JointState>
    {
        public List<JointStateReader> JointStateReaders;
        public string FrameId = "Unity";

        private MessageTypes.Sensor.JointState message;    
        
        protected override void Start()
        {
            base.Start();
        }

     

       public void SendPos()
       {
           UpdateMessage();
            
       }

        public void InitializeMessage()
        {
            int jointStateLength = JointStateReaders.Count;
            Debug.Log(jointStateLength);
            message = new MessageTypes.Sensor.JointState
            {
                header = new MessageTypes.Std.Header { frame_id = FrameId },
                name = new string[jointStateLength],
                position = new double[jointStateLength],
                velocity = new double[jointStateLength],
                effort = new double[jointStateLength]
            };
        }

        private void UpdateMessage()
        {
            message.header.Update();
            Debug.Log(JointStateReaders.Count);
            for (int i = 0; i < JointStateReaders.Count; i++)
            {
                if (JointStateReaders[i] != null)
                {
                    Debug.Log(i);
                    UpdateJointState(i);
                }
                
            }
            Publish(message);
            foreach (var v in JointStateReaders)
            {
                v.callOff();
            }
        }

        private void UpdateJointState(int i)
        {

            JointStateReaders[i].Read(
                out message.name[i],
                out float position,
                out float velocity,
                out float effort);

            message.position[i] = position;
            message.velocity[i] = velocity;
            message.effort[i] = effort;
        }


    }
}