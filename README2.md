# 여러가지 사용법을 정리

OnBackButtonPressed overide 이벤트는 device back button을 이야기 함.
Navigation back button 은 다른 방법으로 해야 하는데 해결 못함
App Icon변경
 -. pixel size 1024, 1024 png 파일
 -. android assent studio > png image 선택 > name 변경 > Save zip >  copy to resource
 - MainActivity Icon명 수정
    . [Activity(Label = "MyNotes", Icon = "@mipmap/mynotesAppIcon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
   

# 필요한 package
SQLite: sqlite-net-pcl
https://docs.microsoft.com/ko-kr/xamarin/get-started/quickstarts/database?pivots=windows

app icon 변경
https://www.youtube.com/watch?v=tbKrbv9h_ZE