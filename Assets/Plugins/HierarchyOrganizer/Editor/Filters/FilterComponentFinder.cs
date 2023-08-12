using UnityEngine;
using System.Collections;

public class ComponentFinder : MonoBehaviour
{

    // Строка для хранения названия компонента
    private string componentName;
    private string _value;
    // Метод, который принимает название компонента и ищет его во всех игровых объектах
    void FindComponent(string value = null)
    {
        _value = value;
        // Создаем массив из всех игровых объектов в сцене
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        // Перебираем все игровые объекты в цикле
        foreach (GameObject go in allObjects)
        {
            // Пытаемся получить компонент с заданным именем из текущего игрового объекта
            Component comp = go.GetComponent(_value);
            // Если компонент не равен null, то он существует в игровом объекте
            if (comp != null)
            {
                componentName = comp.name;
            }
        }
    }

}