using System.Dynamic;
using System.Numerics;
using System.Text;
public class ProtoType : DynamicObject {
    private static ProtoType? entity{get;set;}=null;
    private BigInteger id{get;set;}=BigInteger.MinusOne;
    private string key{get;set;}=string.Empty;
    private dynamic? tag{get;set;}=null;
    private Dictionary<BigInteger, dynamic>keys{get;set;}=new(){};
    public Dictionary<BigInteger, List<BigInteger>>tags{get;set;}=new(){};
    public static BigInteger Increment()
    {
        return ProtoType.Entity().id+=1;
    }
    public static ProtoType Entity()
    {
        if(ProtoType.entity is null)
        {
            return ProtoType.entity=new ProtoType();
        }
        return ProtoType.entity;
    }
    public BigInteger Id{
        get{return this.id;}
        set{this.id=value;}
    }
    public string Key{
        get{return this.key;}
        set{this.key=value;}
    }
    public dynamic? Tag{
        get{return this.tag;}
        set{this.tag=value;}
    }
    public ProtoType[] Keys(){
        return this.keys.Values.Where(set=>set.Key.GetType().Equals(typeof(string))&&!set.Tag.GetType().Equals(typeof(ProtoType))).Select(t=>t).Cast<ProtoType>().ToArray();
    }
    public string[] Tags(string Key){
        return ((dynamic)this.keys.Values.Where(set=>set.Key.ToString()==key)).Tags();
    }
    public string[] Tags(){
        return this.keys.Values.Where(set=>set.Key.GetType().Equals(typeof(string))&&!set.Tag.GetType().Equals(typeof(ProtoType))).Select(t=>t.Tag.ToString()).Cast<string>().ToArray();
    }
    public dynamic[] ProtoTypes(){
        return this.keys.Values.Where(set=>set.Tag.GetType().Equals(typeof(ProtoType))||set.Key.GetType().Equals(typeof(ProtoType))).ToArray();
    }
    public override string ToString()
    {
        if(this.Tag is not null){
            return this.Tag.ToString();
        }
        return "\0";
    }
    public static string ToJSON(ProtoType value)
    {
        return value.ToJSON();
    }
    public override bool TryGetMember(GetMemberBinder binder, out dynamic? result)
    {
        string name = binder.Name;
        result = null;
        KeyValuePair<BigInteger,dynamic>entity=keys.Where(k=>k.Value.Key.Equals(name)).FirstOrDefault(new KeyValuePair<BigInteger,dynamic>(BigInteger.MinusOne,new ProtoType(){Id=Increment(),Key=name}));
        if(entity.Key.Equals(BigInteger.MinusOne)){
            try{
                result = keys[entity.Value.id];
                keys.Add(entity.Value.id,entity.Value);
                return true;
            }catch{return false;}
        }
        if(string.IsNullOrEmpty(entity.Value.Key) is false)
        {
            if(entity.Value.Tag is not null)
            {
                if(entity.Value.Tag.GetType().Equals(typeof(string))){
                    result=entity.Value.ToString();
                    return true;
                }
            }else{
                result = entity.Value.Key;
                return true;
            }
        }
        result = keys[entity.Value.Id];
        return true;
    }
    public override bool TrySetMember(SetMemberBinder binder, dynamic? value)
    {
        string name = binder.Name;
        KeyValuePair<BigInteger,dynamic> keyEntity=keys.Where(k=>k.Value.Key.Equals(name)||(k.Value.Key.Equals(value))).FirstOrDefault(new KeyValuePair<BigInteger,dynamic>(BigInteger.MinusOne,null));
        if(keyEntity.Key.Equals(BigInteger.MinusOne)){
            try{
                dynamic keyTags = new ProtoType(){Id=Increment(),Key=name,Tag=value};
                keys.Add(keyTags.id,keyTags);
                keyEntity=new KeyValuePair<BigInteger,dynamic>(keyTags.Id,keyTags);
            }catch{}
        }
        if(value is not null && value.GetType().Equals(typeof(string)) is false && value.GetType().Equals(typeof(ProtoRune<BigInteger>)) is false)
        {
            try{
                try {
                    BigInteger bigInteger = new BigInteger(value);
                    return true;
                } catch{}
                if(value.GetType().Equals(typeof(System.ValueTuple<BigInteger,BigInteger>))){
                    return true;
                }
            }catch{}
            KeyValuePair<BigInteger,List<BigInteger>>keyTag=tags.Where(t=>t.Key.Equals(keyEntity.Value.Id)).FirstOrDefault(new KeyValuePair<BigInteger,List<BigInteger>>(BigInteger.MinusOne,new(){keyEntity.Value.Id,value.Id}));
            if(keyTag.Key.Equals(BigInteger.MinusOne)){
                try{
                    tags.Add(keyTag.Value.First(),new(){keyTag.Value.Last()});
                    tags.Add(keyTag.Value.Last(),new(){keyTag.Value.First()});
                    return true;
                }catch{return false;}
            }else{tags[keyTag.Key].Add(value.Id);}
        }
        return true;
    }
}

public static class KeyTagExtensions
{
    public static string ToJSON(this ProtoType value)
    {
        StringBuilder supsetNotation = new StringBuilder(), subsetNotation = new StringBuilder();
        string keySetValue = string.Empty;
        dynamic key, keyTag;
        dynamic[] Keys = value.Keys(),KeyTags = value.ProtoTypes(),keys,keyTags;
        long index=0,length=Keys.LongLength;
        for(index=0,length=Keys.LongLength;index<length;index++){
            key=Keys[index];
            keys=key.Keys();
            subsetNotation.Clear();
            if(key.Tag.GetType().Equals(typeof(System.ValueTuple<BigInteger,BigInteger>))){
                try{
                    subsetNotation.Append(string.Join((char)ProtoGyph.Separa.Tion,string.Join((char)ProtoGyph.Func.Tion, key.Tag.Item1, key.Tag.Item2)));
                }catch{}
            }else{
                if(key.Tag.GetType().Equals(typeof(string))){
                    subsetNotation.Append((char)ProtoGyph.Fini.Cal);
                    subsetNotation.Append(string.Join((char)ProtoGyph.Separa.Tion, key.Tag.ToString()));
                    subsetNotation.Append((char)ProtoGyph.Fini.Nal);
                }else{
                    subsetNotation.Append(string.Join((char)ProtoGyph.Separa.Tion, key.Tag.ToString()));
                }
            }
            supsetNotation.Append((char)ProtoGyph.Fini.Cal);
            supsetNotation.Append(key.Key);
            supsetNotation.Append((char)ProtoGyph.Fini.Nal);
            supsetNotation.Append((char)ProtoGyph.Equa.Tion);
            supsetNotation.Append(keys.Length>1?(char)ProtoGyph.Ordi.Cal:string.Empty);
            supsetNotation.Append(subsetNotation.ToString());
            supsetNotation.Append(keys.Length>1?(char)ProtoGyph.Ordi.Nal:string.Empty);
            if(index.Equals(length-1)is false){
                supsetNotation.Append((char)ProtoGyph.Separa.Tion);
            }
        }
        if(index.Equals(0)is false){
            supsetNotation.Append((char)ProtoGyph.Separa.Tion);
        }
        for(index=0,length=KeyTags.LongLength;index<length;index++){
            keyTag=KeyTags[index];
            keys = keyTag.Keys();
            keyTags = keyTag.ProtoTypes();
            subsetNotation.Clear();
            foreach(dynamic _key in keys){
                subsetNotation.Append((char)ProtoGyph.Fini.Cal);
                subsetNotation.Append(_key.Key);
                subsetNotation.Append((char)ProtoGyph.Fini.Nal);
                subsetNotation.Append((char)ProtoGyph.Equa.Tion);
                subsetNotation.Append((char)ProtoGyph.Fini.Cal);
                subsetNotation.Append(_key.ToString());
                subsetNotation.Append((char)ProtoGyph.Fini.Nal);
                subsetNotation.Append((char)ProtoGyph.Separa.Tion);
            }
            foreach(dynamic _Key in keyTags.DistinctBy(key=>key.Key)){
                subsetNotation.Append((char)ProtoGyph.Fini.Cal);
                subsetNotation.Append(_Key.Key);
                subsetNotation.Append((char)ProtoGyph.Fini.Nal);
                subsetNotation.Append((char)ProtoGyph.Equa.Tion);
                subsetNotation.Append(ProtoType.ToJSON(_Key));
                subsetNotation.Append((char)ProtoGyph.Separa.Tion);
            }
            if(index.Equals(length-1)){
                supsetNotation.Append((char)ProtoGyph.Fini.Cal);
                supsetNotation.Append(keyTag.Key);
                supsetNotation.Append((char)ProtoGyph.Fini.Nal);
                supsetNotation.Append((char)ProtoGyph.Equa.Tion);
                supsetNotation.Append((char)ProtoGyph.Lexi.Cal);
                supsetNotation.Append(subsetNotation.ToString().TrimEnd((char)ProtoGyph.Separa.Tion,(char)ProtoGyph.Fini.Nal));
                supsetNotation.Append((char)ProtoGyph.Lexi.Nal);
                subsetNotation.Clear();
            }else{
                subsetNotation.Append((char)ProtoGyph.Separa.Tion);
            }
        }
        subsetNotation.Clear();
        subsetNotation.Append(supsetNotation.ToString());
        supsetNotation.Clear();
        supsetNotation.Append((char)ProtoGyph.Lexi.Cal);
        supsetNotation.Append(subsetNotation.ToString().TrimEnd((char)ProtoGyph.Separa.Tion));
        supsetNotation.Append((char)ProtoGyph.Lexi.Nal);
        return supsetNotation.ToString();
    }
}