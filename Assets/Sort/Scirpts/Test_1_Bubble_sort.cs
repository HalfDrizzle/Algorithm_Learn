using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Test_1_Bubble_sort : MonoBehaviour
{
    public GameObject testPrefabs;
    
    public int dataLength = 10;

    public Color colorStart = Color.white;
    
    public Color colorEnd = Color.black;

    //Time required for each step
    [Range(0,10f)]
    public float time; 
        
    private float dataEachTap;
    
    private Data[] _dataVisualization;

    class ColorZ
    {
        public int startIndex;
        public int endIndex;
        public float data;
        public Color c;
        public void SortColorZ(ColorZ[] dataF)
        {
            for(int end = dataF.Length-1; end > 0; end--){
                bool isSort = true;
                for(int i = 0; i < end; i++){
                    if(dataF[i].data > dataF[i+1].data){
                        swapZ(dataF,i,i+1);
                        isSort = false;
                    }
                }
                if(isSort)break;
            }
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init();
        _dataVisualization = new Data[dataLength];
        dataEachTap = 1f/(float)dataLength;
        float[] dataF = new float[dataLength];
        ColorZ[] array = new ColorZ[dataLength];
        for (int i = 0; i < dataLength; i++)
        {
            dataF[i] = Random.Range(0, 10f);
            ColorZ z = new ColorZ();
            array[i] = z;
            array[i].startIndex = i;
            array[i].data = dataF[i];
        }

        array[0].SortColorZ(array);
        Color c = Color.white;
        for (int i = 0; i < dataLength; i++)
        {
            array[i].c = Color.Lerp(colorStart, colorEnd, i*dataEachTap);
        }
        
        for (int i = 0; i < _dataVisualization.Length; i++)
        {
            GameObject a = Instantiate(testPrefabs);
            float data = dataF[i];
            Vector3 localScale = new Vector3(dataEachTap*10*0.8f,data,1f);
            foreach (var colorZ in array)
            {
                if (colorZ.startIndex == i)
                {
                    c = colorZ.c;
                    a.GetComponent<SpriteRenderer>().color = c;
                    break;
                }
            }
            _dataVisualization[i] = new Data( a.transform, data, a.transform.localScale);            
            Transform dataT = _dataVisualization[i]._transform;
            var position = dataT.position;
            position = Vector3.Lerp(Vector3.zero, Vector3.right*10, i*dataEachTap);
            dataT.localScale = localScale;
            position = new Vector3(position.x,data/2f,position.z);
            dataT.position = position;
            _dataVisualization[i]._posX = position.x;
        }

        StartCoroutine(Change_bubble_sort());
    }


    IEnumerator Change_bubble_sort()
    {
        float[] dataF = new float[dataLength];
        for (int i = 0; i < dataLength; i++)
        {
            dataF[i] = _dataVisualization[i].GetData();
        }
        for(int end = dataF.Length-1; end > 0; end--){
            bool isSort = true;
            for(int i = 0; i < end; i++){
                if(dataF[i] > dataF[i+1]){
                    swap(dataF,i,i+1);
                    ChangeT(i,i+1);
                    yield return new WaitForSeconds(time);
                    isSort = false;
                }
            }
            if(isSort)break;
        }
    }

    void Sort(float[] dataF)
    {
        for(int end = dataF.Length-1; end > 0; end--){
            bool isSort = true;
            for(int i = 0; i < end; i++){
                if(dataF[i] > dataF[i+1]){
                    swap(dataF,i,i+1);
                    isSort = false;
                }
            }
            if(isSort)break;
        }
    }
    
    static void swap(float[] arr,int i,int j) { 
        float temp = arr[i];
        arr[i] = arr[j]; 
        arr[j] = temp;
    }

    static void swapZ<T>(T[] arr,int i,int j)
    {
        T temp = arr[i];
        arr[i] = arr[j]; 
        arr[j] = temp;
    }
    
    //Swap data on UI panel and tra
    void ChangeT(int i , int j)
    {
        float p = _dataVisualization[i]._posX;
        _dataVisualization[i].Move(_dataVisualization[j]._posX,time);
        _dataVisualization[j].Move(p,time);
        Data temp = _dataVisualization[i];
        _dataVisualization[i] = _dataVisualization[j]; 
        _dataVisualization[j] = temp;
    }
}

public class Data
{
    public Transform _transform;

    public float _posX;
        
    private float _data;

    public Data(Transform t,float d,Vector3 localScale)
    {
        _transform = t;
        _data = d;
        _posX = _transform.position.x;
        _transform.localScale = localScale;
    }

    public float GetData()
    {
        return _data;
    }
    
    
    public void SetData(float d)
    {
        _data = d;
    }

    public void Move(float targetX,float time)
    {
        Vector3 a = _transform.position;
        _transform.DOMove(new Vector3(targetX,a.y,a.z),time);
        _posX = targetX;
    }
}