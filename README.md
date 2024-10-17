# VacancyManagementSystem


Proyekt : Vakansiya idarəetmə proqram təminatı
Açıqlama : Elan olunan vakansiyaya dair test nümunələrinin hazırlanması, istifiadəçi tərəfindən bu vakansiyalara aid test proqramında iştirak edilməsi

Proeykt 3 hissədən ibarətdir: 
Admin , User , Api

Backend : .Net 8.0 . C# Web Api
Database : MSSQL
Frontend : Angular v17 .
UI : Angular Material


Setup : 

İlk olaraq APİ-də connectionstring lər dəyişdirilməlidir (appsettings və Infrastructure/Data/VacanyManagementDbContext-də) .
Sonra Angular proyektlərinin hər ikisində olan environment.ts -də baseUrl porta uyğun yazılmalıdır.


Backend proyektinin içərisində SQL viewlar istifadə olunduğu üçün üzərində hazır migration folderi vardır . Console-da "update-database" edərək(Infrastruture layerini startup project edib) bazanı köçürük .

Frontend tərəfə isə Angular Material yüklənməlidir .("ng add @angular/material")

Backenddə qeyd olunan bütün funksionallıqlar nəzərə alınmışdır.
Bəzi funksionallıqlar frontda qoşulmayıb


----Admin
1)ilk olaraq Admin də vacancy Qruplar yaradılmalıdır . 
2)Sonra vakansiyalar qeyd olunamlıdır.
3)Sual bankında vakansiya seçilməlidir. vakansiyaya dair sual bankında suallar ilə cavabları yaradılmaldır

----User
User tərəfdə namizəd vakansiyalardan birinə müraciət edir . Məlumatlar doldurulur . 
User role tələb olunmadığı üçün kontrol Fin üzərindən aparılır.
Sual sayına uyğun vaxt timerdə əks olunur .
User imtahanı bitirdikdən sonra Cv faylını yükləyir.
Bundan sonra Admin tərəfdə userə aid olan imtahanlar və məlumatlar Namizədlər bölməsində əks olunur



