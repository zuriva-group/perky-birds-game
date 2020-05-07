using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

public class Item
{

    [XmlAttribute("Id")]
    public string id;

    [XmlElement("ScoreToPass")]
    public float scoreToPass;

    [XmlElement("BirdMaxTurn")]
    public float birdMaxTurn;

    [XmlElement("DoublePipeProbability")]
    public float DoublePipeProbability;
    
    //
    //  FIRST PIPE
    //

    [XmlElement("Pipe1NormalTypeProbability")]
    public float pipe1NormalTypeProbability;

    [XmlElement("Pipe1ColorTypeProbability")]
    public float pipe1ColorTypeProbability;

    [XmlElement("Pipe1LifeTypeProbability")]
    public float pipe1LifeTypeProbability;

    [XmlElement("Pipe1Speed")]
    public float pipe1Speed;

    [XmlElement("Pipe1MoveRange")]
    public float pipe1MoveRange;

    //
    //  SECOND PIPE
    //

    [XmlElement("Pipe2NormalTypeProbability")]
    public float pipe2NormalTypeProbability;

    [XmlElement("Pipe2ColorTypeProbability")]
    public float pipe2ColorTypeProbability;

    [XmlElement("Pipe2LifeTypeProbability")]
    public float pipe2LifeTypeProbability;

    [XmlElement("Pipe2Speed")]
    public float pipe2Speed;

    [XmlElement("Pipe2MoveRange")]
    public float pipe2MoveRange;

}
