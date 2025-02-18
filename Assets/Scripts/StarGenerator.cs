using Unity.VisualScripting;
using UnityEngine;

public class StarGenerator : MonoBehaviour
{
    public GameObject StarGO;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int MaxStars;
    // Mảng chứa các màu sắc
    Color[] starColors = {
    new Color(0.5f, 0.5f, 1f), // xanh dương
    new Color(0, 1f, 1f),      // xanh lá
    new Color(1f, 1f, 0),      // vàng
    new Color(1f, 0, 0),
    new Color(0 ,0,0),// đỏ
};

    // Sử dụng trong khởi tạo
    void Start()
    {
        // Điểm góc dưới bên trái của màn hình
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        // Điểm góc trên bên phải của màn hình
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        // Vòng lặp tạo các ngôi sao
        for (int i = 0; i < MaxStars; ++i)
        {
            GameObject star = Instantiate(StarGO);

            // Gán màu sắc cho ngôi sao
            star.GetComponent<SpriteRenderer>().color = starColors[i % starColors.Length];

            // Đặt vị trí ngẫu nhiên cho ngôi sao
            star.transform.position = new Vector2(
                Random.Range(min.x, max.x),
                Random.Range(min.y, max.y)
            );

            // Đặt tốc độ ngẫu nhiên cho ngôi sao
            star.GetComponent<StartBG_GO>().speed = -(1f * Random.value + 0.5f);

            // Gán ngôi sao làm con của StarGeneratorGO
            star.transform.parent = transform;
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
