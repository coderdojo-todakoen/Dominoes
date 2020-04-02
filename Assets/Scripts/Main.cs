using UnityEngine;

public class Main : MonoBehaviour
{
    public GameObject prefab;

    // [スペース]キーが押されたらtrueになります
    internal bool start = false;

    // 先頭に並べたドミノが倒れている間trueになります
    internal bool front = false;

    // タグが"Top0"のドミノが倒れた時の時間を記録します
    internal float time;

    // タグが"Top10" のドミノが倒れたらtrueになります
    // カメラの位置を移動するために使用します
    internal bool top = false;

    // タグが"Top55"のドミノが倒れたらtrueになります
    // カメラの位置を移動するために使用します
    internal bool end = false;

    // topがtrueの間のカメラを移動する量を設定します
    internal Vector3 translation;

    // Start is called before the first frame update
    void Start()
    {
        // ドミノを並べる処理です

        // 最初に直線に30個のドミノを並べます
        for (int i = 1; i != 30; i++)
        {
            GameObject domino = GameObject.Instantiate(prefab, new Vector3(-i, 0, -i), Quaternion.Euler(0, 45, 0));
            if (i < 20)
            {
                domino.tag = "Front";
            }
        }

        // Resourcesフォルダに置かれている画像ファイルを読み込みます
        // Inspectorで"Read/Write Enabled"にチェックを入れておく必要があります
        Texture2D texture = Resources.Load("logo") as Texture2D;
        Color[] colors = texture.GetPixels();
        for (int z = 0; z != texture.height; ++z)
        {
            for (int x = 0; x != texture.width; ++x)
            {
                GameObject domino = GameObject.Instantiate(prefab, new Vector3(x, 0, z), Quaternion.Euler(0, 45, 0));
                domino.GetComponent<MeshRenderer>().materials[1].color = colors[z * texture.width + x];
                // カメラの位置を動かすきっかけとなるドミノにタグを設定しておきます
                if (z == 0 && x == 0)
                {
                    domino.tag = "Top0";
                }
                if (z == 10 && x == 10)
                {
                    domino.tag = "Top10";
                }
                if (z == 55 && x == 55)
                {
                    domino.tag = "Top55";
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!start && Input.GetKeyDown(KeyCode.Space))
        {
            // [スペース]キーが押されたら、
            // 最初のドミノに力を加えます
            GameObject first = GameObject.Find("First");
            first.GetComponent<Rigidbody>().AddForce(0, 0, 20);

            start = true;
        }
    }

    void FixedUpdate()
    {
        if (top && !end)
        {
            // カメラを徐々に上に移動します
            Camera.main.transform.position += (translation * Time.deltaTime);
            Camera.main.transform.LookAt(new Vector3(30, 0, 30));
        }
    }
}
