# OrderFlow

OrderFlow, .NET Core ve RabbitMQ tabanlı bir sipariş yönetim sistemidir. Bu proje, mesaj kuyruğu yapısının güçlü bir şekilde kullanıldığı, ölçeklenebilir ve genişletilebilir bir sistem tasarımı sunar. Özellikle RabbitMQ consumer yapısının nasıl organize edildiğini ve OOP prensiplerine dayalı olarak nasıl geliştirildiğini vurgular.

---

## İçindekiler

1. [Kurulum ve Gereksinimler](#kurulum-ve-gereksinimler)
2. [Mimari ve Kullanılan Teknolojiler](#mimari-ve-kullanılan-teknolojiler)
3. [Consumer Yapısı](#consumer-yapısı)
4. [Nasıl Kullanılır?](#nasıl-kullanılır)
5. [Katkıda Bulunma](#katkıda-bulunma)

---

## Kurulum ve Gereksinimler

### Gereksinimler:
- .NET 8.0 (veya daha yeni)
- RabbitMQ server (Yerel veya Docker üzerinde çalıştırılabilir)

### Projeyi Çalıştırma:

1. RabbitMQ server'ınızı çalıştırın (Docker kullanıyorsanız şu komutu çalıştırabilirsiniz):
    ```bash
    docker run -d --name rabbitmq -p 15672:15672 -p 5672:5672 rabbitmq:management
    ```
2. Projeyi indirin:
    ```bash
    git clone https://github.com/muates/OrderFlow.git
    cd OrderFlow
    ```
3. Gerekli bağımlılıkları yükleyin:
    ```bash
    dotnet restore
    ```
4. Uygulamayı başlatın:
    ```bash
    dotnet run
    ```

---

## Mimari ve Kullanılan Teknolojiler

OrderFlow, aşağıdaki teknolojilerle geliştirilmiştir:
- **.NET Core**: Uygulamanın temel framework'ü.
- **RabbitMQ**: Mesaj kuyruğu sistemi.
- **OOP Prensipleri**: Genişletilebilir ve sürdürülebilir bir yapı için nesne yönelimli programlama prensipleri.

### Proje Katmanları:
- **Producer**: Mesajların RabbitMQ'ya gönderilmesini sağlayan katman. Burada mesajları hazırlayıp kuyruğa gönderme işlemleri yapılır.
- **Consumer Services**: RabbitMQ'dan gelen mesajları dinleyen ve işleyen katman. Her bir consumer, gelen mesajı alır ve belirli işlemleri gerçekleştirir.
- **Shared Library**: Hem Producer hem de Consumer arasında paylaşılan ortak sınıfların bulunduğu katman. Mesaj yapıları, servisler ve yardımcı sınıflar bu katmanda yer alır.

---

## Consumer Yapısı

OrderFlow'da, RabbitMQ mesajlarını işleyen `Consumer` yapısı önemli bir yer tutmaktadır. Bu yapı, **OOP prensiplerine** dayalı olarak aşağıdaki şekilde organize edilmiştir:

### 1. **IRabbitMqConsumer Arayüzü**
`IRabbitMqConsumer`, tüm consumer'ların sahip olması gereken temel davranışları tanımlar. Her bir consumer bu arayüzü implemente ederek, RabbitMQ'dan gelen mesajları alır ve işler.

### 2. **RabbitMqConsumer<TMessage> Sınıfı**
`RabbitMqConsumer<TMessage>`, RabbitMQ'dan mesajları alıp işlemek için temel bir sınıf sağlar. Her consumer, bu sınıfı kalıtım yoluyla genişletir.

Bu sınıfın temel işlevi, **mesajın alınması** ve **işlenmesi** sürecini standartlaştırmaktır. Ancak, her consumer'ın kendine özel iş mantığını işlemek için `ProcessMessage` metodunun soyutlanmış olması sağlanır. Bu metod, her consumer'da **gelen mesajın işlenmesinden sorumlu** olacaktır.

### 3. **Consumer Örneği: PaymentConsumer**
`PaymentConsumer`, bir ödeme işlemi için örnek bir consumer'dır. Bu sınıf, gelen mesajları işler ve gerekli işlemi gerçekleştirir.

**PaymentConsumer** sınıfı, `RabbitMqConsumer<Order>` sınıfını kalıtarak çalışır ve gelen mesajı işlemek için `ProcessMessage` metodunu implement eder.

Bu sayede, **her consumer** kendine özgü işlem mantığına sahip olur ve kolayca genişletilebilir.

### 4. **RabbitMqBackgroundService**
**RabbitMqBackgroundService**, tüm **Consumer** sınıflarını arka planda çalıştıran servistir. Bu sınıf, RabbitMQ ile bağlantıyı başlatır ve **mesajları sürekli olarak dinler**. Tüm consumer'lar bu sınıf tarafından yönetilir. Böylece, uygulama çalıştığı sürece, mesajlar sürekli olarak alınır ve işlenir.

---

## Nasıl Kullanılır?

OrderFlow, **RabbitMQ**'yu kullanan bir **producer-consumer** yapısını benimser. Bir mesajı göndermek veya tüketmek için aşağıdaki adımları izleyebilirsiniz:

### 1. Yeni bir Consumer Eklemek

Yeni bir consumer eklemek oldukça kolaydır. Örneğin, `ShippingConsumer` adında yeni bir consumer ekleyelim:

1. **Consumer Sınıfını Tanımlayın**: `ShippingConsumer` sınıfı `RabbitMqConsumer<TMessage>` sınıfından kalıtım alır ve `ProcessMessage` metodunu implement eder.
2. **Startup veya Program.cs Dosyasına Ekleyin**: `ShippingConsumer` sınıfını DI (Dependency Injection) konteynerine ekleyin ve RabbitMQ ile bağlantıyı yapılandırın.

### 2. Yeni bir Mesaj Göndermek

RabbitMQ'ya mesaj göndermek için `IMessageSender<TMessage>` arayüzünü kullanabilirsiniz. Bu arayüz, belirli bir tipteki mesajları RabbitMQ'ya göndermeyi sağlar.

---

## Katkıda Bulunma

Bu projeye katkıda bulunmak isterseniz, aşağıdaki adımları takip edebilirsiniz:
1. Bu repository'yi fork'layın.
2. Yeni bir branch oluşturun: `git checkout -b feature/your-feature`.
3. Değişikliklerinizi yapın ve commit edin: `git commit -am 'Add new feature'`.
4. Push edin: `git push origin feature/your-feature`.
5. Pull request açın.

---

## Lisans

Bu proje MIT Lisansı altında lisanslanmıştır. Detaylar için [LICENSE](LICENSE) dosyasına bakabilirsiniz.
