﻿using System.Collections.Generic;
using UnityEngine;

namespace PaintIn3D
{
	/// <summary>This class manages the replace channels painting command.</summary>
	public class P3dCommandReplaceChannels : P3dCommand
	{
		public P3dHashedTexture TextureR;
		public P3dHashedTexture TextureG;
		public P3dHashedTexture TextureB;
		public P3dHashedTexture TextureA;
		public Vector4          ChannelR;
		public Vector4          ChannelG;
		public Vector4          ChannelB;
		public Vector4          ChannelA;

		public static P3dCommandReplaceChannels Instance = new P3dCommandReplaceChannels();

		private static Stack<P3dCommandReplaceChannels> pool = new Stack<P3dCommandReplaceChannels>();

		private static Material cachedMaterial;

		private static int cachedMaterialHash;

		public override bool RequireMesh { get { return false; } }

		static P3dCommandReplaceChannels()
		{
			BuildMaterial(ref cachedMaterial, ref cachedMaterialHash, "Hidden/Paint in 3D/Replace Channels");
		}

		public static void Blit(RenderTexture renderTexture, Texture textureR, Texture textureG, Texture textureB, Texture textureA, Vector4 channelR, Vector4 channelG, Vector4 channelB, Vector4 channelA, Vector4 channels)
		{
			var material = Instance.SetMaterial(textureR, textureG, textureB, textureA, channelR, channelG, channelB, channelA);

			Instance.Apply(material);

			P3dHelper.Blit(renderTexture, material, Instance.Pass);
		}

		public static void BlitFast(RenderTexture renderTexture, Texture textureR, Texture textureG, Texture textureB, Texture textureA, Vector4 channelR, Vector4 channelG, Vector4 channelB, Vector4 channelA)
		{
			var material = Instance.SetMaterial(textureR, textureG, textureB, textureA, channelR, channelG, channelB, channelA);

			Instance.Apply(material);

			Graphics.Blit(default(Texture), renderTexture, material);
		}

		public override void Apply(Material material)
		{
			base.Apply(material);

			material.SetTexture(P3dShader._TextureR, TextureR);
			material.SetTexture(P3dShader._TextureG, TextureG);
			material.SetTexture(P3dShader._TextureB, TextureB);
			material.SetTexture(P3dShader._TextureA, TextureA);
			material.SetVector(P3dShader._ChannelR, ChannelR);
			material.SetVector(P3dShader._ChannelG, ChannelG);
			material.SetVector(P3dShader._ChannelB, ChannelB);
			material.SetVector(P3dShader._ChannelA, ChannelA);
		}

		public override void Pool()
		{
			pool.Push(this);
		}

		public override void Transform(Matrix4x4 posMatrix, Matrix4x4 rotMatrix)
		{
		}

		public override P3dCommand SpawnCopy()
		{
			var command = SpawnCopy(pool);

			command.TextureR = TextureR;
			command.TextureG = TextureG;
			command.TextureB = TextureB;
			command.TextureA = TextureA;
			command.ChannelR = ChannelR;
			command.ChannelG = ChannelG;
			command.ChannelB = ChannelB;
			command.ChannelA = ChannelA;

			return command;
		}

		public Material SetMaterial(Texture textureR, Texture textureG, Texture textureB, Texture textureA, Vector4 channelR, Vector4 channelG, Vector4 channelB, Vector4 channelA)
		{
			Material = new P3dHashedMaterial(cachedMaterial, cachedMaterialHash);
			TextureR = textureR;
			TextureG = textureG;
			TextureB = textureB;
			TextureA = textureA;
			ChannelR = channelR;
			ChannelG = channelG;
			ChannelB = channelB;
			ChannelA = channelA;

			return cachedMaterial;
		}
	}
}