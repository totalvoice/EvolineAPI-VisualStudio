# DEPRECATED
Se você usa a versão do .NET framework > 4.5. Utilize a versão atualizada da lib: [totalvoice-csharp](https://github.com/totalvoice/totalvoice-csharp)

# EvolineAPI-VisualStudio
Lib (dll) C# (VisualStudio) para integração com a API Evoline

## Como utilizar (how to)

```c#
    Evoline.API.EvolineAPI api = new Evoline.API.EvolineAPI("{{access-token}}");
    api.debugOn();
    dynamic saldo = api.consultaSaldo();
    Console.WriteLine(saldo.dados.saldo);

    dynamic chamada = api.enviaChamada("********", "*********", true);
    Console.WriteLine(chamada);
    Console.ReadLine();

    dynamic sms = api.enviaSMS("***********", "Mensagem de teste de SMS");
    Console.WriteLine(sms);
    Console.ReadLine();
```

## Pré requisitos

- .NET 4.5
- Newtonsoft.Json (http://www.newtonsoft.com/json)


## Licença

MIT
