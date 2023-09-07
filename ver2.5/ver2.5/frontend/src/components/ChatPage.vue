<template>


<el-container>
      <el-aside width="350px">
        <span> 聊天列表 </span>
        <el-button @click="goBacktoPer">返回上一界面</el-button>
        <div v-for="v in chatlist" :key="v.user">
          <el-card v-if="v.user">
            <span>{{v.user}}</span>
            <br>
            <div v-if="v.chat_type">
              <span>[图片]</span>
            </div>
            <div v-else>
              <span>{{v.message}}</span>
            </div>
            <br>
            <el-button @click="exchange_chat_member(v.user)">聊天</el-button>
          </el-card>
        </div>
      </el-aside>
      <el-main>
    
  <el-card class="box-card">
    <template #header>
      <div class="card-header">
        <span>{{chat_member}}</span>
        <el-button class="button" text>友善发言，和平上网</el-button>
      </div>
    </template>
    <el-scrollbar height="400px" ref="scrollbarRef" always>
      <div ref="innerRef">
    <div v-for="v in messages" :key="v.id">
      <div v-if="v.user===user_id">
        <div v-if="v.chat_type">
          <el-card v-if="v.id!=''" class="my-chat-bubble" >
            {{ v.id+" "+v.user + " : image"}}
            <img :src="'http://124.70.7.210:5000/'+v.message" />
          </el-card>
        </div>
        <div v-else>
          <el-card v-if="v.id!=''" class="my-chat-bubble">
            {{ v.id+" "+v.user + " : " + v.message }}
          </el-card>
        </div>
      </div>
      <div v-else>
        <div v-if="v.chat_type">
          <el-card v-if="v.id!=''" class="chat-bubble" >
            {{ v.id+" "+v.user + " : image"}}
            <img :src="'http://124.70.7.210:5000/'+v.message" />
          </el-card>
        </div>
        <div v-else>
          <el-card v-if="v.id!=''" class="chat-bubble">
            {{ v.id+" "+v.user + " : " + v.message }}
          </el-card>
        </div>
      </div>
    </div>
  </div>
    </el-scrollbar>
  </el-card>

      <el-row style="margin:20px,0;">
        <el-col :lg="16">
          <el-input v-model="messageText" autosize type="textarea" class="chat-input">
          </el-input>
          <el-upload
            ref="uploadRef"
            class="upload-demo"
            action="https://run.mocky.io/v3/9d059bf9-4660-45f2-925d-ce80ad6c4d15"
            :auto-upload="false"
            v-model:file-list='fileList'
            limit="1"
          >
            <template #trigger>
              <el-button class="image-input" type="primary">上传图片</el-button>
            </template>

          </el-upload>
          <br>
          <el-button  class="chat-send" size="large" @click="sendMessage" :disabled="isdiabled">发送</el-button>
        </el-col>
      </el-row>
    </el-main>
  </el-container>
</template>

<script setup lang="ts">
import * as signalR from '@microsoft/signalr'
import { ConsoleLogger } from '@microsoft/signalr/dist/esm/Utils';
import { Message,ElScrollbar as ElScrollbarType } from 'element-plus';

const innerRef = ref<HTMLDivElement>()
const scrollbarRef = ref<InstanceType<typeof ElScrollbarType>>()

import { onMounted, ref } from 'vue';
import { toRaw } from'@vue/reactivity';

    const connection = ref<any>(null);
    const messageText = ref('');
    const messages = ref([{
      id:"",
      user:"",
      message:"",
      chat_type:false
    }]);

    const chatlist = ref([{
      user:"",
      message:"",
      chat_type:false
    }]);
    
  import { useRouter, useRoute } from 'vue-router';
  import axios from 'axios';
  import { fa } from 'element-plus/es/locale';
  const route = useRoute();
  const router = useRouter();
  
  //const user_ID=ref("")//ref(route.query.id as any)
  const user_id=ref("")
  const chat_member=ref("")
  const cus_ID=ref("")
  const sto_ID=ref("")
  const user_type=ref("")
  const isdiabled=ref(true)
  const fileList = ref([]);


  type Chat= {
      chatTime: string;
      cusID: string;
      storeID: string;
      chatContent:string;
      chatSender:boolean;
      chatType:boolean;
    }
onMounted(() => {
    console.log(cus_ID.value)
    console.log(sto_ID.value)
    user_type.value=sessionStorage.getItem('user_type') as string
    if(user_type.value==="0"){
      cus_ID.value=sessionStorage.getItem('cus_id') as string
      user_id.value=cus_ID.value as any
    }else{
      sto_ID.value=sessionStorage.getItem('sto_id') as string
      user_id.value=sto_ID.value as any
    }
    console.log(user_id.value)
    connection.value = new signalR.HubConnectionBuilder()
      .withUrl('http://124.70.7.210:5000/chat?user_Id='+user_id.value,{
      skipNegotiation: true,
      transport: signalR.HttpTransportType.WebSockets
    }) // Use the Hub endpoint URL
    .build();
  
  const onReceiveMessage = async (message_:Chat) => {
      const obj=toRaw(message_)
      const user=ref('')
      if(obj.chatSender===false){
      user.value=obj.cusID
    }else{
      user.value=obj.storeID
    }
    if(chat_member.value==user.value){
      await messages.value.push({ id: obj.chatTime, user:user.value, message:obj.chatContent,chat_type:obj.chatType });
      scrollbarRef.value!.setScrollTop(innerRef.value!.clientHeight)
    }
      connection.value.invoke('GetChatList',user_id.value)
    };
  const onGetChatHistory=async (chatHistory: Array<Chat>)=>{
      messages.value=[{
      id:"",
      user:"",
      message:"",
      chat_type:true
    }]
      const ChatList=toRaw(chatHistory)
      //console.log("ChatlistObj:  "+ChatList[1].chatType)
    await ChatList.forEach(content => {
      //console.log(content["ChatContent"].toString())
      if(content.chatSender===false)
        messages.value.push({ id: content.chatTime, user:content.cusID.toString(), message:content.chatContent,chat_type:content.chatType })
      else
        messages.value.push({ id: content.chatTime, user:content.storeID.toString(), message:content.chatContent,chat_type:content.chatType })
    });
    scrollbarRef.value!.setScrollTop(innerRef.value!.clientHeight)
    }

  const onGetChatList=(chatList:Array<Chat>)=>{
      chatList=toRaw(chatList)
      console.log("Chatlist:  "+chatList)
      chatlist.value=[{
      user:"",
      message:"",
      chat_type:false
    }]
      chatList.forEach(content => {
      //console.log(content["ChatContent"].toString())
      if(user_type.value==="0")
        chatlist.value.push({ user:content.storeID.toString(), message:content.chatContent,chat_type:content.chatType })
      else
        chatlist.value.push({ user:content.cusID.toString(), message:content.chatContent ,chat_type:content.chatType})
    });
    
  }

  connection.value.on('ReceiveMessage', onReceiveMessage);
  connection.value.on('ReceiveChatHistory',onGetChatHistory);
  connection.value.on('ReceiveChatList',onGetChatList)

  connection.value.start()
    .then(async () => {
      console.log('Connected to SignalR Hub');
      await connection.value.invoke('GetChatList',user_id.value)
      console.log(chatlist.value)
      if(route.query.sto_id!=""){
        if(user_type.value==="0"){
          chat_member.value=route.query.sto_id as string
          sto_ID.value=chat_member.value
          isdiabled.value=false
        }
          connection.value.invoke('GetChatHistory',cus_ID.value,sto_ID.value)

      }else if(chatlist.value.length>1){
        if(user_type.value==="0"){
          chat_member.value=chatlist.value[1].user
          sto_ID.value=chat_member.value
          isdiabled.value=false
        }
        else if(user_type.value==="1"){
          chat_member.value=chatlist.value[1].user
          cus_ID.value=chat_member.value
          isdiabled.value=false
        }
          connection.value.invoke('GetChatHistory',cus_ID.value,sto_ID.value)
      }
      else{
        chat_member.value="您暂时还没有聊天对象！"
      }

    })
    .catch((error: any) => {
      console.error(error);
    });
});



const sendMessage = async () => {
  if(messageText.value!=''){
    const currentTimestamp = Math.floor(Date.now() / 1000);

    // 使用Date对象将时间戳转换为标准日期时间格式
    const date = new Date(currentTimestamp * 1000); // Multiply by 1000 to convert seconds to milliseconds

    // 使用toLocaleString()方法获取标准日期时间格式
    const formattedDateTime = date.toLocaleString('chinese',{ hour12: false });
    const sendmessage=ref(null as any)
    if(user_type.value==="1"){
      sendmessage.value=connection.value.invoke('SendMessage',formattedDateTime,cus_ID.value,sto_ID.value, messageText.value,true,false)
    }else{
      sendmessage.value=connection.value.invoke('SendMessage',formattedDateTime,cus_ID.value,sto_ID.value, messageText.value,false,false)
    }
    
    await messages.value.push({ id: formattedDateTime, user:user_id.value.toString(), message:messageText.value ,chat_type:false});
  }else if(fileList.value.length>0){
    const formData = new FormData();
    await fileList.value.forEach((file) => {
        formData.append('image', file.raw); // 将文件添加到FormData中
      });
    const response = await axios.post('/api/uploadchatimage', formData, {
      headers: {
        'Content-Type': 'multipart/form-data',
      },
    });

    if (response.status === 200) {
      const data = response.data;
      if (data.message === 'success') {
        messageText.value=data.url
      } 
    } else {
      console.error(`Error: HTTP status code ${response.status}`);
    }
    
    const currentTimestamp = Math.floor(Date.now() / 1000);

    // 使用Date对象将时间戳转换为标准日期时间格式
    const date = new Date(currentTimestamp * 1000); // Multiply by 1000 to convert seconds to milliseconds

    // 使用toLocaleString()方法获取标准日期时间格式
    const formattedDateTime = date.toLocaleString('chinese',{ hour12: false });

    if(user_type.value==="1"){
      connection.value.invoke('SendMessage',formattedDateTime,cus_ID.value,sto_ID.value, messageText.value,true,true)
    }else{
      connection.value.invoke('SendMessage',formattedDateTime,cus_ID.value,sto_ID.value, messageText.value,false,true)
    }

    await messages.value.push({ id: formattedDateTime, user:user_id.value.toString(), message:messageText.value,chat_type:true });

    fileList.value=[]

  }
    messageText.value = '';
    //messageText.value = '';
    scrollbarRef.value!.setScrollTop(innerRef.value!.clientHeight)
};

const exchange_chat_member=(now_id:string)=>{
    console.log(user_type.value)
    if(user_type.value=="0"){
      sto_ID.value=now_id
      chat_member.value=now_id
    }
    else{
      cus_ID.value=now_id
      chat_member.value=now_id
    }
    isdiabled.value=false
    console.log(cus_ID.value+"+"+sto_ID.value)
    connection.value.invoke('GetChatHistory',cus_ID.value,sto_ID.value)
}

const goBacktoPer=()=>{
  router.go(-1)
}
</script>

<style scoped>
.card-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.text {
  font-size: 14px;
}

.item {
  margin-bottom: 18px;
}

.box-card {
  width: 1105px;
  height: 500px;
}

.chat-send{
  display: flex;
  margin-left: 120%;
  width: 150px;
}

.chat-input{
  display: flex;
  margin-left: 6%;
  width: 1050px;
}

.image-input{
  display: flex;
  margin-right: -675%;
  width:250px;
}

.chat-bubble {
  background-color: #007bff; /* 设置气泡的背景颜色 */
  color: white; /* 设置气泡文本的颜色 */
  padding: 5px;
  border-radius: 10px;
  max-width: 40%; /* 设置气泡的最大宽度 */
  margin-top: 30px;
}

.my-chat-bubble {
  background-color: #24db67; /* 设置气泡的背景颜色 */
  color: white; /* 设置气泡文本的颜色 */
  padding: 5px;
  border-radius: 10px;
  max-width: 40%; /* 设置气泡的最大宽度 */
  margin-top: 30px;
  margin-left: 50%;
}
</style>