﻿using UnityEngine;
using UnityEngine.Rendering;

namespace Mixture
{
	[System.Serializable]
	public abstract class FixedShaderNode : ShaderNode
	{
		public abstract string  shaderName { get; }
		public abstract bool    displayMaterialInspector { get; }
		public override Texture previewTexture => output;

		protected override MixtureRTSettings defaultRTSettings
		{
			get
			{
                var settings = base.defaultRTSettings;
                settings.editFlags = EditFlags.All ^ EditFlags.POTSize;
                return settings;
			}
		}

		protected override void Enable()
		{
			if (shader == null)
				FindShader();

			base.Enable();
        }

		void FindShader()
		{
			shader = Shader.Find(shaderName);
			if (material != null && material.shader != shader)
				material.shader = shader;
		}

		public override bool canProcess
		{
			get
			{
				if (shader == null)
				{
					FindShader();
					material.shader = shader;
				}
			
				return base.canProcess;
			}
		}
	}
}