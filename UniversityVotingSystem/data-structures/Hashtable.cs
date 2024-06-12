namespace UniversityVotingSystem;
public class Hashtable
{
    private Node[] _nodes;
    private int _tableSize;
    private int _allNodesValuesSum; //Num of all elements
    private int _filledNodes;
    public Hashtable()
    {
        _nodes = new Node[10];
        _tableSize = 10;
        _allNodesValuesSum = 0;
        _filledNodes = 0;
    }

    private int hashFunction(int key)
    {
        int hashValue = key % _tableSize;
        return hashValue;
    }

    public void updateRow(int key, NodeValue nodeValue)
    {
        int hashValue = hashFunction(key);

        Node node = new Node(key, nodeValue);
        if(_nodes[hashValue] is not null)
        {
            if(_nodes[hashValue].getKey() == key) _nodes[hashValue].setValue(1);
            else
            {
                bool isNewNodeCreated = _nodes[hashValue].createNode(node);
                _filledNodes = isNewNodeCreated ? _filledNodes + 1 : _filledNodes;
            }
        }
        else
        {
            _nodes[hashValue] = node;
            _filledNodes++;
        }
        _allNodesValuesSum += 1;
    }

    public Node[]? GetNodes()
    {
        if(_filledNodes == 0)
        {
            return null;
        }

        Node[] filledNodesArray = new Node[_filledNodes];

        int i = 0; //index for main array of nodes
        int j = 0; //index for filledNodesArray

        while(i < _tableSize && j < _filledNodes)
        {
            if(_nodes[i] is null)
            {
                i++;
                continue;
            }

            Node? currentNode = _nodes[i];
            while(currentNode is not null)
            {
                filledNodesArray[j] = new Node(currentNode.getKey(), currentNode.getValue());
                currentNode = currentNode.getNextNode();
                j++;
            }
            i++;
        }

        return filledNodesArray;
    }

}

public class Node
{
    private readonly int key;
    private NodeValue value;
    private Node? nextNode;

    public Node (int key, NodeValue value)
    {
        this.key = key;
        this.value = value;
    }

    public int getKey()
    {
        return key;
    }

    public NodeValue? getValue()
    {
        if(value is not null){
            return value;
        }
        return null;
    }

    public int GetValueCount(){
        if(value is not null){
            return value.Count;
        }
        return -1;
    }

    public void setValue(int value)
    {
        this.value.Count += value;
    }
    public void decrementValue()
    {
        value.Count--;
    }

    public Node? getNextNode()
    {
        if(nextNode is null) return null;
        else
        {
            return nextNode;
        }
    }

    public NodeValue? ChangeValue(NodeValue? value){
        return value;
    }

    public bool createNode(Node node)
    {
        if(nextNode == null)
        {
            nextNode = node;
            return true;
        }

        Node currentNode = nextNode;
        while(currentNode.nextNode != null)
        {
            if (currentNode.getKey() == node.getKey())
            {
                currentNode.ChangeValue(node.getValue());
                return false;
            }

            currentNode = currentNode.nextNode;
        }

        currentNode.nextNode = node;
        return true;
    }
}