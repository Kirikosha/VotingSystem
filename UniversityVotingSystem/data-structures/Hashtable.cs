namespace UniversityVotingSystem;
public class Hashtable
{
    Node[] nodes;
    private int tableSize;
    private int allNodesValuesSum; //Num of all elements
    private int filledNodes;
    public Hashtable()
    {
        nodes = new Node[10];
        tableSize = 10;
        allNodesValuesSum = 0;
        filledNodes = 0;
    }

    private int hashFunction(int key)
    {
        int hashValue = key % tableSize;
        return hashValue;
    }

    public void updateRow(int key, int value = 1)
    {
        int hashValue = hashFunction(key);

        Node node = new Node(key, value);
        if(nodes[hashValue] is not null)
        {
            if(nodes[hashValue].getKey() == key) nodes[hashValue].setValue(value);
            else
            {
                bool isNewNodeCreated = nodes[hashValue].createNode(node);
                filledNodes = isNewNodeCreated ? filledNodes + 1 : filledNodes;
            }
        }
        else
        {
            nodes[hashValue] = node;
            filledNodes++;
        }
        allNodesValuesSum += 1;
    }

    public Node[]? GetNodes()
    {
        if(filledNodes == 0)
        {
            return null;
        }

        Node[] filledNodesArray = new Node[filledNodes];

        int i = 0; //index for main array of nodes
        int j = 0; //index for filledNodesArray

        while(i < tableSize && j < filledNodes)
        {
            if(nodes[i] is null)
            {
                i++;
                continue;
            }

            Node? currentNode = nodes[i];
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
    private int value;
    private Node? nextNode;

    public Node (int key, int value)
    {
        this.key = key;
        this.value = value;
    }

    public int getKey()
    {
        return key;
    }

    public int getValue()
    {
        return value;
    }

    public void setValue(int value)
    {
        this.value += value;
    }
    public void decrementValue()
    {
        value--;
    }

    public Node? getNextNode()
    {
        if(nextNode is null) return null;
        else
        {
            return nextNode;
        }
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
                currentNode.setValue(node.getValue());
                return false;
            }

            currentNode = currentNode.nextNode;
        }

        currentNode.nextNode = node;
        return true;
    }
}