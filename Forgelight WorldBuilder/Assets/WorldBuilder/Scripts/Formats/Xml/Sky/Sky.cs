namespace WorldBuilder.Formats.Xml.Sky
{

        // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public class Sky
        {
            private SkyInclude[] includesField;

            private SkyTransition transitionField;

            private SkyTimeOfDay timeOfDayField;

            private SkyWind windField;

            private SkySunLight sunLightField;

            private SkyPost postField;

            private SkyValue[] nightVisionField;

            private SkyLens lensField;

            private SkyProbe probeField;

            private SkyTexture[] domeField;

            private SkyFog fogField;

            private object satellitesField;

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("Include", IsNullable = false)]
            public SkyInclude[] Includes
            {
                get
                {
                    return this.includesField;
                }
                set
                {
                    this.includesField = value;
                }
            }

            /// <remarks/>
            public SkyTransition Transition
            {
                get
                {
                    return this.transitionField;
                }
                set
                {
                    this.transitionField = value;
                }
            }

            /// <remarks/>
            public SkyTimeOfDay TimeOfDay
            {
                get
                {
                    return this.timeOfDayField;
                }
                set
                {
                    this.timeOfDayField = value;
                }
            }

            /// <remarks/>
            public SkyWind Wind
            {
                get
                {
                    return this.windField;
                }
                set
                {
                    this.windField = value;
                }
            }

            /// <remarks/>
            public SkySunLight SunLight
            {
                get
                {
                    return this.sunLightField;
                }
                set
                {
                    this.sunLightField = value;
                }
            }

            /// <remarks/>
            public SkyPost Post
            {
                get
                {
                    return this.postField;
                }
                set
                {
                    this.postField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("Value", IsNullable = false)]
            public SkyValue[] NightVision
            {
                get
                {
                    return this.nightVisionField;
                }
                set
                {
                    this.nightVisionField = value;
                }
            }

            /// <remarks/>
            public SkyLens Lens
            {
                get
                {
                    return this.lensField;
                }
                set
                {
                    this.lensField = value;
                }
            }

            /// <remarks/>
            public SkyProbe Probe
            {
                get
                {
                    return this.probeField;
                }
                set
                {
                    this.probeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("Texture", IsNullable = false)]
            public SkyTexture[] Dome
            {
                get
                {
                    return this.domeField;
                }
                set
                {
                    this.domeField = value;
                }
            }

            /// <remarks/>
            public SkyFog Fog
            {
                get
                {
                    return this.fogField;
                }
                set
                {
                    this.fogField = value;
                }
            }

            /// <remarks/>
            public object Satellites
            {
                get
                {
                    return this.satellitesField;
                }
                set
                {
                    this.satellitesField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class SkyInclude
        {

            private string fileField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string File
            {
                get
                {
                    return this.fileField;
                }
                set
                {
                    this.fileField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class SkyTransition
        {

            private byte timeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Time
            {
                get
                {
                    return this.timeField;
                }
                set
                {
                    this.timeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class SkyTimeOfDay
        {

            private SkyTimeOfDayDawn dawnField;

            private SkyTimeOfDayDay dayField;

            private SkyTimeOfDayDusk duskField;

            private SkyTimeOfDayNight nightField;

            /// <remarks/>
            public SkyTimeOfDayDawn Dawn
            {
                get
                {
                    return this.dawnField;
                }
                set
                {
                    this.dawnField = value;
                }
            }

            /// <remarks/>
            public SkyTimeOfDayDay Day
            {
                get
                {
                    return this.dayField;
                }
                set
                {
                    this.dayField = value;
                }
            }

            /// <remarks/>
            public SkyTimeOfDayDusk Dusk
            {
                get
                {
                    return this.duskField;
                }
                set
                {
                    this.duskField = value;
                }
            }

            /// <remarks/>
            public SkyTimeOfDayNight Night
            {
                get
                {
                    return this.nightField;
                }
                set
                {
                    this.nightField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class SkyTimeOfDayDawn
        {

            private string startTimeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string StartTime
            {
                get
                {
                    return this.startTimeField;
                }
                set
                {
                    this.startTimeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class SkyTimeOfDayDay
        {

            private string startTimeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string StartTime
            {
                get
                {
                    return this.startTimeField;
                }
                set
                {
                    this.startTimeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class SkyTimeOfDayDusk
        {

            private string startTimeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string StartTime
            {
                get
                {
                    return this.startTimeField;
                }
                set
                {
                    this.startTimeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class SkyTimeOfDayNight
        {

            private string startTimeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string StartTime
            {
                get
                {
                    return this.startTimeField;
                }
                set
                {
                    this.startTimeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class SkyWind
        {

            private SkyWindSway swayField;

            private decimal xField;

            private decimal yField;

            private decimal zField;

            private decimal scaleField;

            /// <remarks/>
            public SkyWindSway Sway
            {
                get
                {
                    return this.swayField;
                }
                set
                {
                    this.swayField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal X
            {
                get
                {
                    return this.xField;
                }
                set
                {
                    this.xField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal Y
            {
                get
                {
                    return this.yField;
                }
                set
                {
                    this.yField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal Z
            {
                get
                {
                    return this.zField;
                }
                set
                {
                    this.zField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal Scale
            {
                get
                {
                    return this.scaleField;
                }
                set
                {
                    this.scaleField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class SkyWindSway
        {

            private decimal spacingField;

            private decimal wavefrontScaleField;

            private decimal speedField;

            private decimal boilField;

            private decimal dirField;

            private decimal smoothingField;

            private decimal strengthField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal Spacing
            {
                get
                {
                    return this.spacingField;
                }
                set
                {
                    this.spacingField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal WavefrontScale
            {
                get
                {
                    return this.wavefrontScaleField;
                }
                set
                {
                    this.wavefrontScaleField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal Speed
            {
                get
                {
                    return this.speedField;
                }
                set
                {
                    this.speedField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal Boil
            {
                get
                {
                    return this.boilField;
                }
                set
                {
                    this.boilField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal Dir
            {
                get
                {
                    return this.dirField;
                }
                set
                {
                    this.dirField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal Smoothing
            {
                get
                {
                    return this.smoothingField;
                }
                set
                {
                    this.smoothingField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal Strength
            {
                get
                {
                    return this.strengthField;
                }
                set
                {
                    this.strengthField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class SkySunLight
        {

            private SkySunLightShadows shadowsField;

            private SkySunLightDirection[] directionField;

            private SkySunLightColor[] colorField;

            /// <remarks/>
            public SkySunLightShadows Shadows
            {
                get
                {
                    return this.shadowsField;
                }
                set
                {
                    this.shadowsField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("Direction")]
            public SkySunLightDirection[] Direction
            {
                get
                {
                    return this.directionField;
                }
                set
                {
                    this.directionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("Color")]
            public SkySunLightColor[] Color
            {
                get
                {
                    return this.colorField;
                }
                set
                {
                    this.colorField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class SkySunLightShadows
        {

            private bool enabledField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public bool Enabled
            {
                get
                {
                    return this.enabledField;
                }
                set
                {
                    this.enabledField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class SkySunLightDirection
        {

            private string timeField;

            private decimal headingField;

            private decimal pitchField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Time
            {
                get
                {
                    return this.timeField;
                }
                set
                {
                    this.timeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal Heading
            {
                get
                {
                    return this.headingField;
                }
                set
                {
                    this.headingField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal Pitch
            {
                get
                {
                    return this.pitchField;
                }
                set
                {
                    this.pitchField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class SkySunLightColor
        {

            private string timeField;

            private decimal rField;

            private decimal gField;

            private decimal bField;

            private decimal brightnessField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Time
            {
                get
                {
                    return this.timeField;
                }
                set
                {
                    this.timeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal R
            {
                get
                {
                    return this.rField;
                }
                set
                {
                    this.rField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal G
            {
                get
                {
                    return this.gField;
                }
                set
                {
                    this.gField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal B
            {
                get
                {
                    return this.bField;
                }
                set
                {
                    this.bField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal Brightness
            {
                get
                {
                    return this.brightnessField;
                }
                set
                {
                    this.brightnessField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class SkyPost
        {

            private SkyPostAmbientOcclusion ambientOcclusionField;

            private SkyPostBloom[] bloomField;

            private SkyPostExposure[] exposureField;

            private SkyPostHaze hazeField;

            private SkyPostTexture[] colorGradingField;

            /// <remarks/>
            public SkyPostAmbientOcclusion AmbientOcclusion
            {
                get
                {
                    return this.ambientOcclusionField;
                }
                set
                {
                    this.ambientOcclusionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("Bloom")]
            public SkyPostBloom[] Bloom
            {
                get
                {
                    return this.bloomField;
                }
                set
                {
                    this.bloomField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("Exposure")]
            public SkyPostExposure[] Exposure
            {
                get
                {
                    return this.exposureField;
                }
                set
                {
                    this.exposureField = value;
                }
            }

            /// <remarks/>
            public SkyPostHaze Haze
            {
                get
                {
                    return this.hazeField;
                }
                set
                {
                    this.hazeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("Texture", IsNullable = false)]
            public SkyPostTexture[] ColorGrading
            {
                get
                {
                    return this.colorGradingField;
                }
                set
                {
                    this.colorGradingField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class SkyPostAmbientOcclusion
        {

            private string timeField;

            private decimal sharpnessField;

            private decimal sizeField;

            private decimal strengthField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Time
            {
                get
                {
                    return this.timeField;
                }
                set
                {
                    this.timeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal Sharpness
            {
                get
                {
                    return this.sharpnessField;
                }
                set
                {
                    this.sharpnessField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal Size
            {
                get
                {
                    return this.sizeField;
                }
                set
                {
                    this.sizeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal Strength
            {
                get
                {
                    return this.strengthField;
                }
                set
                {
                    this.strengthField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class SkyPostBloom
        {

            private string timeField;

            private decimal strengthField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Time
            {
                get
                {
                    return this.timeField;
                }
                set
                {
                    this.timeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal Strength
            {
                get
                {
                    return this.strengthField;
                }
                set
                {
                    this.strengthField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class SkyPostExposure
        {

            private string timeField;

            private decimal whitePointField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Time
            {
                get
                {
                    return this.timeField;
                }
                set
                {
                    this.timeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal WhitePoint
            {
                get
                {
                    return this.whitePointField;
                }
                set
                {
                    this.whitePointField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class SkyPostHaze
        {

            private ushort distanceField;

            private decimal strengthField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public ushort Distance
            {
                get
                {
                    return this.distanceField;
                }
                set
                {
                    this.distanceField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal Strength
            {
                get
                {
                    return this.strengthField;
                }
                set
                {
                    this.strengthField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class SkyPostTexture
        {

            private string fileField;

            private string timeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string File
            {
                get
                {
                    return this.fileField;
                }
                set
                {
                    this.fileField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Time
            {
                get
                {
                    return this.timeField;
                }
                set
                {
                    this.timeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class SkyValue
        {

            private string timeField;

            private decimal intensityField;

            private byte whitePointAdjustmentField;

            private decimal noiseField;

            private decimal scanLinesField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Time
            {
                get
                {
                    return this.timeField;
                }
                set
                {
                    this.timeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal Intensity
            {
                get
                {
                    return this.intensityField;
                }
                set
                {
                    this.intensityField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte WhitePointAdjustment
            {
                get
                {
                    return this.whitePointAdjustmentField;
                }
                set
                {
                    this.whitePointAdjustmentField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal Noise
            {
                get
                {
                    return this.noiseField;
                }
                set
                {
                    this.noiseField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal ScanLines
            {
                get
                {
                    return this.scanLinesField;
                }
                set
                {
                    this.scanLinesField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class SkyLens
        {

            private SkyLensHood hoodField;

            private SkyLensHaze hazeField;

            private SkyLensDirtLayer1 dirtLayer1Field;

            private SkyLensDirtLayer2 dirtLayer2Field;

            private SkyLensDirtNoise dirtNoiseField;

            private SkyLensDirt dirtField;

            private SkyLensDirtFalloff dirtFalloffField;

            private bool enabledField;

            /// <remarks/>
            public SkyLensHood Hood
            {
                get
                {
                    return this.hoodField;
                }
                set
                {
                    this.hoodField = value;
                }
            }

            /// <remarks/>
            public SkyLensHaze Haze
            {
                get
                {
                    return this.hazeField;
                }
                set
                {
                    this.hazeField = value;
                }
            }

            /// <remarks/>
            public SkyLensDirtLayer1 DirtLayer1
            {
                get
                {
                    return this.dirtLayer1Field;
                }
                set
                {
                    this.dirtLayer1Field = value;
                }
            }

            /// <remarks/>
            public SkyLensDirtLayer2 DirtLayer2
            {
                get
                {
                    return this.dirtLayer2Field;
                }
                set
                {
                    this.dirtLayer2Field = value;
                }
            }

            /// <remarks/>
            public SkyLensDirtNoise DirtNoise
            {
                get
                {
                    return this.dirtNoiseField;
                }
                set
                {
                    this.dirtNoiseField = value;
                }
            }

            /// <remarks/>
            public SkyLensDirt Dirt
            {
                get
                {
                    return this.dirtField;
                }
                set
                {
                    this.dirtField = value;
                }
            }

            /// <remarks/>
            public SkyLensDirtFalloff DirtFalloff
            {
                get
                {
                    return this.dirtFalloffField;
                }
                set
                {
                    this.dirtFalloffField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public bool Enabled
            {
                get
                {
                    return this.enabledField;
                }
                set
                {
                    this.enabledField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class SkyLensHood
        {

            private byte falloffField;

            private decimal angleField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Falloff
            {
                get
                {
                    return this.falloffField;
                }
                set
                {
                    this.falloffField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal Angle
            {
                get
                {
                    return this.angleField;
                }
                set
                {
                    this.angleField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class SkyLensHaze
        {

            private decimal sunStrField;

            private decimal skyStrField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal SunStr
            {
                get
                {
                    return this.sunStrField;
                }
                set
                {
                    this.sunStrField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal SkyStr
            {
                get
                {
                    return this.skyStrField;
                }
                set
                {
                    this.skyStrField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class SkyLensDirtLayer1
        {

            private string timeField;

            private byte sizeMinField;

            private byte sizeMaxField;

            private decimal strengthMinField;

            private decimal strengthMaxField;

            private byte densityField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Time
            {
                get
                {
                    return this.timeField;
                }
                set
                {
                    this.timeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte SizeMin
            {
                get
                {
                    return this.sizeMinField;
                }
                set
                {
                    this.sizeMinField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte SizeMax
            {
                get
                {
                    return this.sizeMaxField;
                }
                set
                {
                    this.sizeMaxField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal StrengthMin
            {
                get
                {
                    return this.strengthMinField;
                }
                set
                {
                    this.strengthMinField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal StrengthMax
            {
                get
                {
                    return this.strengthMaxField;
                }
                set
                {
                    this.strengthMaxField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Density
            {
                get
                {
                    return this.densityField;
                }
                set
                {
                    this.densityField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class SkyLensDirtLayer2
        {

            private string timeField;

            private decimal sizeMinField;

            private decimal sizeMaxField;

            private decimal strengthMinField;

            private decimal strengthMaxField;

            private decimal densityField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Time
            {
                get
                {
                    return this.timeField;
                }
                set
                {
                    this.timeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal SizeMin
            {
                get
                {
                    return this.sizeMinField;
                }
                set
                {
                    this.sizeMinField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal SizeMax
            {
                get
                {
                    return this.sizeMaxField;
                }
                set
                {
                    this.sizeMaxField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal StrengthMin
            {
                get
                {
                    return this.strengthMinField;
                }
                set
                {
                    this.strengthMinField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal StrengthMax
            {
                get
                {
                    return this.strengthMaxField;
                }
                set
                {
                    this.strengthMaxField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal Density
            {
                get
                {
                    return this.densityField;
                }
                set
                {
                    this.densityField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class SkyLensDirtNoise
        {

            private byte scaleField;

            private decimal strengthField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Scale
            {
                get
                {
                    return this.scaleField;
                }
                set
                {
                    this.scaleField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal Strength
            {
                get
                {
                    return this.strengthField;
                }
                set
                {
                    this.strengthField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class SkyLensDirt
        {

            private string timeField;

            private byte bloomStrField;

            private decimal sunStrField;

            private decimal skyStrField;

            private decimal alphaField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Time
            {
                get
                {
                    return this.timeField;
                }
                set
                {
                    this.timeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte BloomStr
            {
                get
                {
                    return this.bloomStrField;
                }
                set
                {
                    this.bloomStrField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal SunStr
            {
                get
                {
                    return this.sunStrField;
                }
                set
                {
                    this.sunStrField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal SkyStr
            {
                get
                {
                    return this.skyStrField;
                }
                set
                {
                    this.skyStrField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal Alpha
            {
                get
                {
                    return this.alphaField;
                }
                set
                {
                    this.alphaField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class SkyLensDirtFalloff
        {

            private decimal falloffField;

            private byte cutoffField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal Falloff
            {
                get
                {
                    return this.falloffField;
                }
                set
                {
                    this.falloffField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Cutoff
            {
                get
                {
                    return this.cutoffField;
                }
                set
                {
                    this.cutoffField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class SkyProbe
        {

            private SkyProbeColorMap colorMapField;

            private SkyProbeNormalMap normalMapField;

            private SkyProbeLocation locationField;

            /// <remarks/>
            public SkyProbeColorMap ColorMap
            {
                get
                {
                    return this.colorMapField;
                }
                set
                {
                    this.colorMapField = value;
                }
            }

            /// <remarks/>
            public SkyProbeNormalMap NormalMap
            {
                get
                {
                    return this.normalMapField;
                }
                set
                {
                    this.normalMapField = value;
                }
            }

            /// <remarks/>
            public SkyProbeLocation Location
            {
                get
                {
                    return this.locationField;
                }
                set
                {
                    this.locationField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class SkyProbeColorMap
        {

            private string fileField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string File
            {
                get
                {
                    return this.fileField;
                }
                set
                {
                    this.fileField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class SkyProbeNormalMap
        {

            private string fileField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string File
            {
                get
                {
                    return this.fileField;
                }
                set
                {
                    this.fileField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class SkyProbeLocation
        {

            private decimal xField;

            private decimal yField;

            private decimal zField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal X
            {
                get
                {
                    return this.xField;
                }
                set
                {
                    this.xField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal Y
            {
                get
                {
                    return this.yField;
                }
                set
                {
                    this.yField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal Z
            {
                get
                {
                    return this.zField;
                }
                set
                {
                    this.zField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class SkyTexture
        {

            private string timeField;

            private string fileField;

            private decimal evCompField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Time
            {
                get
                {
                    return this.timeField;
                }
                set
                {
                    this.timeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string File
            {
                get
                {
                    return this.fileField;
                }
                set
                {
                    this.fileField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal EvComp
            {
                get
                {
                    return this.evCompField;
                }
                set
                {
                    this.evCompField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class SkyFog
        {

            private SkyFogColor colorField;

            private SkyFogDistribution[] distributionField;

            private SkyFogRange rangeField;

            private SkyFogFadeRange fadeRangeField;

            private SkyFogTurbulence turbulenceField;

            /// <remarks/>
            public SkyFogColor Color
            {
                get
                {
                    return this.colorField;
                }
                set
                {
                    this.colorField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("Distribution")]
            public SkyFogDistribution[] Distribution
            {
                get
                {
                    return this.distributionField;
                }
                set
                {
                    this.distributionField = value;
                }
            }

            /// <remarks/>
            public SkyFogRange Range
            {
                get
                {
                    return this.rangeField;
                }
                set
                {
                    this.rangeField = value;
                }
            }

            /// <remarks/>
            public SkyFogFadeRange FadeRange
            {
                get
                {
                    return this.fadeRangeField;
                }
                set
                {
                    this.fadeRangeField = value;
                }
            }

            /// <remarks/>
            public SkyFogTurbulence Turbulence
            {
                get
                {
                    return this.turbulenceField;
                }
                set
                {
                    this.turbulenceField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class SkyFogColor
        {

            private string timeField;

            private byte rField;

            private byte gField;

            private byte bField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Time
            {
                get
                {
                    return this.timeField;
                }
                set
                {
                    this.timeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte R
            {
                get
                {
                    return this.rField;
                }
                set
                {
                    this.rField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte G
            {
                get
                {
                    return this.gField;
                }
                set
                {
                    this.gField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte B
            {
                get
                {
                    return this.bField;
                }
                set
                {
                    this.bField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class SkyFogDistribution
        {

            private string timeField;

            private decimal densityField;

            private decimal gradientField;

            private byte floorField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Time
            {
                get
                {
                    return this.timeField;
                }
                set
                {
                    this.timeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal Density
            {
                get
                {
                    return this.densityField;
                }
                set
                {
                    this.densityField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal Gradient
            {
                get
                {
                    return this.gradientField;
                }
                set
                {
                    this.gradientField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Floor
            {
                get
                {
                    return this.floorField;
                }
                set
                {
                    this.floorField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class SkyFogRange
        {

            private byte nearField;

            private ushort farField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Near
            {
                get
                {
                    return this.nearField;
                }
                set
                {
                    this.nearField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public ushort Far
            {
                get
                {
                    return this.farField;
                }
                set
                {
                    this.farField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class SkyFogFadeRange
        {

            private ushort nearField;

            private ushort farField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public ushort Near
            {
                get
                {
                    return this.nearField;
                }
                set
                {
                    this.nearField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public ushort Far
            {
                get
                {
                    return this.farField;
                }
                set
                {
                    this.farField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class SkyFogTurbulence
        {

            private byte enableField;

            private byte scaleField;

            private byte headingField;

            private byte pitchField;

            private byte speedField;

            private decimal boilSpeedField;

            private decimal densityRangeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Enable
            {
                get
                {
                    return this.enableField;
                }
                set
                {
                    this.enableField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Scale
            {
                get
                {
                    return this.scaleField;
                }
                set
                {
                    this.scaleField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Heading
            {
                get
                {
                    return this.headingField;
                }
                set
                {
                    this.headingField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Pitch
            {
                get
                {
                    return this.pitchField;
                }
                set
                {
                    this.pitchField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Speed
            {
                get
                {
                    return this.speedField;
                }
                set
                {
                    this.speedField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal BoilSpeed
            {
                get
                {
                    return this.boilSpeedField;
                }
                set
                {
                    this.boilSpeedField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal DensityRange
            {
                get
                {
                    return this.densityRangeField;
                }
                set
                {
                    this.densityRangeField = value;
                }
            }
        }
}