#region Librerias
using UnityEngine;
#endregion

namespace MoonAntonio
{
	[ExecuteInEditMode]
	public class ScreenSpaceSnow : MonoBehaviour 
	{
		#region Variables
		public Texture2D SnowTexture;
		public Color SnowColor = Color.white;
		public float SnowTextureScale = 0.1f;
		[Range(0, 1)] public float BottomThreshold = 0f;
		[Range(0, 1)] public float TopThreshold = 1f;
		private Material _material;
		#endregion

		#region Metodos
		private void OnEnable()
		{
			// Crea dinamicamente un material que utilizara nuestro shader
			_material = new Material(Shader.Find("MoonAntonio/ScreenSpaceSnow"));

			// Decir a la camara que cree profundidad y normales.
			this.GetComponent<Camera>().depthTextureMode |= DepthTextureMode.DepthNormals;
		}

		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			// Establecer propiedades de shader
			_material.SetMatrix("_CamToWorld", GetComponent<Camera>().cameraToWorldMatrix);
			_material.SetColor("_SnowColor", SnowColor);
			_material.SetFloat("_BottomThreshold", BottomThreshold);
			_material.SetFloat("_TopThreshold", TopThreshold);
			_material.SetTexture("_SnowTex", SnowTexture);
			_material.SetFloat("_SnowTexScale", SnowTextureScale);

			// Ejecutar el shader en la textura de entrada(src) y escribir en la salida(dest)
			Graphics.Blit(src, dest, _material);
		}
		#endregion
	}
}