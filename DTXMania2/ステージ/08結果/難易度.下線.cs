﻿using System;
using System.Collections.Generic;
using System.Linq;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Animation;
using FDK;

namespace DTXMania2.結果
{
    partial class 難易度
    {
        class 下線 : IDisposable
        {

            // プロパティ


            public bool アニメ完了 => ( null != this._ストーリーボード && this._ストーリーボード.Status == StoryboardStatus.Ready );



            // 生成と終了


            public 下線()
            {
            }

            public void Dispose()
            {
                this._長さdpx?.Dispose();
                this._ストーリーボード?.Dispose();
            }



            // 進行と描画


            public void 開始する()
            {
                this._ストーリーボード?.Dispose();
                this._ストーリーボード = new Storyboard( Global.Animation.Manager );

                #region " 長さdpx のアニメ構築 "
                //----------------
                // 初期値 0.0
                this._長さdpx?.Dispose();
                this._長さdpx = new Variable( Global.Animation.Manager, initialValue: 0.0 );

                // 待つ
                using( var 遷移 = Global.Animation.TrasitionLibrary.Constant( duration: 難易度._最初の待機時間sec ) )
                    this._ストーリーボード.AddTransition( this._長さdpx, 遷移 );

                // 全長dpx へ
                using( var 遷移 = Global.Animation.TrasitionLibrary.AccelerateDecelerate( duration: 難易度._アニメ時間sec / 3, finalValue: _全長dpx, accelerationRatio: 0.8, decelerationRatio: 0.2 ) )
                    this._ストーリーボード.AddTransition( this._長さdpx, 遷移 );
                //----------------
                #endregion

                // アニメーション開始
                this._ストーリーボード.Schedule( Global.Animation.Timer.Time );
            }

            public void アニメを完了する()
            {
                this._ストーリーボード?.Finish( 0.1 );
            }

            public void 進行描画する( DeviceContext dc, float left, float top )
            {
                float 長さdpx = (float) this._長さdpx.Value;
                using( var brush = new SolidColorBrush( dc, Color4.White ) )
                    dc.FillRectangle( new RectangleF( left + ( _全長dpx - 長さdpx ) / 2f, top, 長さdpx, 3f ), brush );
            }



            // ローカル

            private const float _全長dpx = 513f;

            private Storyboard _ストーリーボード = null!;

            private Variable _長さdpx = null!;
        }
    }
}
