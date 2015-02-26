namespace Microsoft.Samples.Kinect.BodyBasics
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using Microsoft.Kinect;

	// ジャンケンの判定に関わる計算はこのクラスの内部で行う 
	class Janken
	{
		private bool existUnavail;	// 利用不可能な手
		private int existClosed;	// グー
		private int existLasso;		// チョキ
		private int existOpen;		// パー

		// インストラクタ
		public Janken()
		{
			Reset();
		}

		// Kinectセンサ更新のイベントを受信する度に全フィールドを初期化
		public void Reset()
		{
			existUnavail = false;
			existClosed = 0;
			existLasso = 0;
			existOpen = 0;
		}

		// MainWindow.DrawHand
		public void Add( HandState handState )
		{
			switch ( handState ) {
				case HandState.Closed:
					existClosed = 1;
					break;

				case HandState.Lasso:
					existLasso = 1;
					break;

				case HandState.Open:
					existOpen = 1;
					break;
				// 手がトラッキングされていない(NotTracked)
				// または手がグーチョキパー以外の形(Unknown)
				default:
					existUnavail = true;
					break;
			}
		}

		public String Judge()
		{
			if ( existUnavail ) {
				return "出揃っていません";
			}

			int hands = existClosed + existLasso + existOpen;

			// 1: 全部同じ手
			// 3: 全部バラバラ
			if ( hands == 1 || hands == 3 ) {
				return "あいこです";
			}

			// チョキとパー
			if ( existClosed == 0 ) {
				return "チョキの勝ちです";
			}

			// グーとパー
			if ( existLasso == 0 ) {
				return "パーの勝ちです";
			}

			// グーとチョキ
			if ( existOpen == 0 ) {
				return "グーの勝ちです";
			}

			return "不明な状態です";
		}
	}
}
