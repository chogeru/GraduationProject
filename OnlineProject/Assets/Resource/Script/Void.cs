using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Void : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        /*
         クラスが有効の時に、クラスがゲーム内でインスタンス化（実体化）した瞬間の１度だけ呼び出されるメソッド。
         */
    }

    // Update is called once per frame
    void Update()
    {
        /*
         クラスが有効の時に、１フレーム（※）に１回呼び出されるメソッド。
         */
    }
    void Awake()
    {
        /*
         Start()よりも先に呼び出されます。Awake()が呼び出される順番は不定であるため、
        このメソッドに他のオブジェクトを参照させていると対象のオブジェクトが初期化されていないために上手く動作しない可能性があります。
        そのため、Awake()では自身に関する初期化処理を行います。
        主にシーンのロード（初期化）に使われます。また、他のオブジェクトを参照しつつ初期化を行うイベントはStart()で定義します。
         */
    }
    void LateUpdate()
    {
        /*
         LateUpdate()はUpdate()と同様に１フレームに１回呼び出されます。Update()との違いは、１フレームの中で呼び出されるタイミングです。
        Update()が様々な処理の途中で呼び出されるのに対し、LateUpdate()は１フレーム中の全ての処理が終わった最後に呼び出されます。
         */
    }
    void FixedUpdate()
    {
        /*
         ゲーム内時間での一定間隔で呼ばれます。現実の時間とは別で動いているため、フレームレートに依存しません。
        そのため、１フレーム中に１度も呼ばれなかったり、複数回呼ばれたりすることがあります。ゲーム内時間更新後、物理演算処理の前に毎回呼ばれます。
         */

    }
}
