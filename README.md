# Electro Adventure  
## Unity3d 2인 협동 퍼즐 게임  
### 개요  
컴퓨터(PC)는 많은 보급률로 인해 사람들에게 굉장히 익숙한 플랫폼이다. 하지만 대부분 사용법에 대해서는 알지만, 컴퓨터에 어떤 부품들이 있고 어떤 역할을 하는지에 대해서는 알지 못한다. 플레이어는 컴퓨터의 대표적인 부품인 GPU, 파워, CPU 등이 있는 본체 속으로 들어가 작아진 캐릭터를 플레이하게 되며, 작아진 캐릭터를 통해 부품을 자세히 살펴볼 수 있다.  


### 일정  
<img width="100%" src="https://user-images.githubusercontent.com/31684326/170504602-f7ea89e2-bce0-4a84-afc7-b94948ac2617.jpg">  

### 아키텍쳐  
<img width="100%" src="https://user-images.githubusercontent.com/31684326/170494668-5c7fa162-4a06-4c9d-b2ae-c4e12b9c0791.PNG">  

### 스크립트  
<img width="100%" src="https://user-images.githubusercontent.com/31684326/170492010-2edd7fa6-4aa5-43ca-a45e-939efa7f17ca.png">   

### 설계  
<img width="100%" src="https://user-images.githubusercontent.com/31684326/170507789-33a03f03-24b6-4238-9417-7ef539ac72ba.png">   

### 테스트  
<img width="100%" src="https://user-images.githubusercontent.com/31684326/170503325-2c9c4f3b-dafc-4541-a13a-63f71d657064.jpg">   


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
