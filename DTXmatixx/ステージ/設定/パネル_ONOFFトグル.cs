﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SharpDX;
using FDK;
using FDK.メディア;

namespace DTXmatixx.ステージ.設定
{
    /// <summary>
    ///		OFF と ON を切り替えられるスイッチ。
    /// </summary>
    class パネル_ONOFFトグル : パネル_文字列リスト
    {
        public bool ONである
            => !( this.OFFである );

        public bool OFFである
            => ( 0 == this.現在選択されている選択肢の番号 );

        public パネル_ONOFFトグル( string パネル名, bool 初期状態はON, Action<パネル> 値の変更処理 = null )
            : base( パネル名, ( 初期状態はON ) ? 1 : 0, new[] { "OFF", "ON" }, 値の変更処理 )
        {
        }
    }
}
