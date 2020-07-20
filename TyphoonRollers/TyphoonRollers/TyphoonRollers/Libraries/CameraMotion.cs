#region Using Statements
using Microsoft.Xna.Framework;
#endregion

namespace TyphoonRollers
{
    /// <summary>
    /// カメラモーションクラス
    /// </summary>
    public class CameraMotion
    {
        #region メソッド
        /// <summary>
        /// カメラの現在位置を中心に回転する
        /// </summary>
        /// <param name="camera">カメラ</param>
        /// <param name="speed">速度</param>
        public static void RotationMotion(ref Camera camera, float speed)
        {
            // Y軸周りに回転
            camera.Rotation(0.0f, speed, 0.0f);
        }

        /// <summary>
        /// 三人称視点
        /// </summary>
        /// <param name="camera">カメラ</param>
        /// <param name="target">注視点</param>
        /// <param name="rotation">回転</param>
        public static void ThirdPersonMotion(ref Camera camera, Vector3 target, Vector3 rotation)
        {
            // ターゲットに設定する
            camera.Target = target;
            // 回転する
            camera.Rotation(rotation);
        }
        #endregion
    }
}
