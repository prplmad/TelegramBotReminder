# Бот умеет: 
1) Сохранять напоминания и заметки, а также пользователей и их состояние в БД
2) Отправлять напоминание пользователю, создавшему его (в то время, которое обозначил пользователь)
3) Удалять заметку или напоминание по Id
4) Выводить все заметки или напоминания пользователя по его запросу

# Для запуска проекта необходимо: 
+ Настроить строку подключения к БД в файле appsettings.Development.json
+ В этом же файле прописать токен, выданный ботом BotFather
+ Я использовал ngrok (https://ngrok.com) для доступа к боту из внешней сети, для его настройки необходимо:
    + запустить ngrok и выполнить команду: ngrok http https://localhost:5001 -host-header="localhost:5001"
    + скопировать https адрес из Forwarding (например, https://9109-188-243-53-193.ngrok.io)
    + добавить данный адрес в Url в файле appsettings.Development.json
+ Далее необходимо активировать WebHook Telegram - для этого вставить в адресную строку браузера команду (скобочки {} удалить)
https://api.telegram.org/bot{Telegram Bot Token из пункта 2}/setWebhook?url={URL из пункта 3}/api/message 
+ После настроки выбираем для запуска NoteProjectBotV4 и запускаем приложение
