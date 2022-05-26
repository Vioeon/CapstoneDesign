# Electro Adventure  
## Unity3d 2인 협동 퍼즐 게임  
### 개요  
고장난 컴퓨터를 고치기 위해 본체의 부품속으로 들어가 협동을 통해 퍼즐을 풀며 수리를 하는 게임  

### 일정  
### 아키텍쳐  
<img width="80%" src="https://user-images.githubusercontent.com/31684326/170494668-5c7fa162-4a06-4c9d-b2ae-c4e12b9c0791.PNG">  

### 스크립트  
<img width="80%" src="https://user-images.githubusercontent.com/31684326/170492010-2edd7fa6-4aa5-43ca-a45e-939efa7f17ca.png">   

### 설계  
#### UI 설계서  
### 테스트  

## Title화면  
<img width="80%" src="https://user-images.githubusercontent.com/31684326/170236001-ee510f02-f357-429a-8699-7160aa799af1.jpg">   

* 닉네임을 입력하여 플레이어 이름 설정  

* Create Room을 통해 룸을 생성  

* Find Room에서 생성된 룸 리스트를 확인할 수 있으며 해당룸을 클릭하여 룸 참여 가능  
  
### 메인로비   
<img width="80%" src="https://user-images.githubusercontent.com/31684326/170236839-3634be4f-af7d-402e-a5bc-13a98af5dfbf.jpg">   
<br>  

### 인게임 화면  
#### GPU스테이지 내부  
<img width="80%" src="https://user-images.githubusercontent.com/31684326/170237358-0ea92865-d2de-457e-8990-2464f7fe4c70.jpg">   
* 스테이지 내에 숨겨져있는 3개의 등급별을 찾아 획득하여 메인로비에 있는 부품의 등급을 변경할 수 있다.  
