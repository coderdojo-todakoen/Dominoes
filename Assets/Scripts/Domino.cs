using UnityEngine;

public class Domino : MonoBehaviour
{
    private Main main;

    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = GameObject.Find("Main");
        main = obj.GetComponent<Main>();
    }

    // ドミノが倒れて他のコライダと衝突したら呼び出されます
    void OnCollisionEnter(Collision collision)
    {
        if ((main == null) || !main.start || (collision.collider.tag == "Plane"))
        {
            // まだ動きはじめていない時と、床との接触では何もしません
            return;
        }

        switch (tag)
        {
            case "Front":
                if (main.front)
                {
                    return;
                }
                main.front = true;

                // カメラを少し後方に移動します
                Vector3 v = new Vector3(transform.position.x - 4, 3, transform.position.z - 4) - 
                                    Camera.main.transform.position;
                Camera.main.transform.Translate(v, Space.World);
                Camera.main.transform.rotation = Quaternion.Euler(0, 45, 0);
                break;
            
            case "Top0":
                if (main.time == 0)
                {
                    // タグが"Top0"のドミノが倒れた時の時間を記録します
                    main.time = Time.time;
                }
                break;

            case "Top10":
                if (main.top)
                {
                    return;
                }
                main.top = true;

                // カメラを移動する量を求めます
                main.translation = new Vector3(30, 60, 30) - Camera.main.transform.position;
                // 時間当たりの移動量を計算します
                main.translation /= (5 * (Time.time - main.time));
                break;
            
            case "Top55":
                if (main.end)
                {
                    return;
                }
                main.end = true;

                // カメラを真上に移動します
                Camera.main.transform.position = new Vector3(30, 60, 30);
                Camera.main.transform.rotation = Quaternion.Euler(90, 0, 0);
                break;

            default:
                break;
        }
    }
}
