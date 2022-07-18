// This code is part of the Fungus library (https://github.com/snozbot/fungus)
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

/*This script has been, partially or completely, generated by the Fungus.GenerateVariableWindow*/
using UnityEngine;

namespace Fungus
{
    /// <summary>
    /// Get or Set a property of a AudioMixerGroup component
    /// </summary>
    [CommandInfo("Property",
                 "AudioMixerGroup",
                 "Get or Set a property of a AudioMixerGroup component")]
    [AddComponentMenu("")]
    public class AudioMixerGroupProperty : BaseVariableProperty
    {
		//generated property
        public enum Property 
        { 
            AudioMixer, 
        }


        [SerializeField]
        protected Property property;

        [SerializeField]
        protected AudioMixerGroupData audioMixerGroupData;

        [SerializeField]
        [VariableProperty(typeof(AudioMixerVariable))]
        protected Variable inOutVar;

        public override void OnEnter()
        {
            var ioam = inOutVar as AudioMixerVariable;


            var target = audioMixerGroupData.Value;

            switch (getOrSet)
            {
                case GetSet.Get:
                    switch (property)
                    {
                        case Property.AudioMixer:
                            ioam.Value = target.audioMixer;
                            break;
                        default:
                            Debug.Log("Unsupported get or set attempted");
                            break;
                    }

                    break;

                case GetSet.Set:
                    switch (property)
                    {
                        default:
                            Debug.Log("Unsupported get or set attempted");
                            break;
                    }

                    break;

                default:
                    break;
            }

            Continue();
        }

        public override string GetSummary()
        {
            if (audioMixerGroupData.Value == null)
            {
                return "Error: no audioMixerGroup set";
            }
            if (inOutVar == null)
            {
                return "Error: no variable set to push or pull data to or from";
            }

            return getOrSet.ToString() + " " + property.ToString();
        }

        public override Color GetButtonColor()
        {
            return new Color32(235, 191, 217, 255);
        }

        public override bool HasReference(Variable variable)
        {
            if (audioMixerGroupData.audioMixerGroupRef == variable || inOutVar == variable)
                return true;

            return false;
        }
    }
}