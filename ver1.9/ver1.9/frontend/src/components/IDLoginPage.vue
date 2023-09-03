<template>
    <div>
      <h1 class="header">通过ID登录</h1>
    <div>
    <el-form label-width="100px" style="max-width: 460px;position: relative;margin: auto;">
      <el-form-item label="账号ID" >
        <el-input type="tel" v-model="loginUsername" placeholder="请输入账号" required/>
      </el-form-item>
      <el-form-item label="密码">
        <el-input type="password" show-password v-model="loginPassword" placeholder="请输入密码" required/>
      </el-form-item>
      <div class="submit-button" style="max-width: 120px;margin: auto;">
        <el-button type="default" round="true" :icon="Pointer" @click="login_id">提交</el-button>
      </div>
    </el-form>
    </div>
  </div>
</template>

<script lang="ts" setup>
import axios from 'axios';
import {ref} from 'vue';
//import { useRouter } from 'vue-router';
import { Pointer } from '@element-plus/icons-vue'
import { ElMessage,ElMessageBox } from 'element-plus';
import type {Action} from 'element-plus';
import { useRouter } from 'vue-router';
  const loginUsername=ref('')
  const loginPassword=ref('')
  const router = useRouter();
  //const router=useRouter()
  /*地址修改此处*/ 
  const address='http://localhost:5000/api/'
  
  /*           */
const login_id = async ()=> {
  console.log('登录测试');
  console.log({
    user_ID_phonenumber:loginUsername.value,
          user_password:loginPassword.value,
          login_type:"0" 
  })
          axios.post(address+'login',  JSON.stringify({ 
          user_ID_phonenumber:loginUsername.value,
          user_password:loginPassword.value,
          login_type:"0" 
        }), {
          headers: {
              'Content-Type': 'application/json'
          }
          })
          .then(response => {
            if(response.data.message==='success'){
                /*------------------------*/
                /*登录成功后编辑此处跳转界面*/
                /*------------------------*/
                if (response.data.user_type=='1'){
                  sessionStorage.removeItem("sto_id");
                  sessionStorage.removeItem("cus_id");
                  sessionStorage.removeItem("user_type");
                  sessionStorage.setItem("sto_id", loginUsername.value);
                  sessionStorage.setItem("user_type", response.data.user_type);
                    router.push({
                      path:'/store'
                  });
                }
                else if (response.data.user_type=='0'){
                  sessionStorage.removeItem("sto_id");
                  sessionStorage.removeItem("cus_id");
                  sessionStorage.removeItem("user_type");
                  sessionStorage.setItem("cus_id", loginUsername.value);
                  sessionStorage.setItem("user_type", response.data.user_type);
                  console.log("loginUsername.value:"+loginUsername.value)
                    router.push({
                      path:'/home'
                  });
                }
              }
              else {
                ElMessageBox.alert(response.data.message, '登录失败', {
                  confirmButtonText: 'OK',
                  callback: (action: Action) => {
                    ElMessage({
                      type: 'info',
                      message: `action: ${action}`,
                    })
                  },
                })
              }
          })
          .catch((error) => {
              console.log('An error occurred:', error);
          });
      };
</script>

<style scoped>
.total-layout{
    display: flex;
    flex-direction:column;
    
}

.form-layout{
  display: flex;
  justify-content: center;

}
.header{
    display: flex;
    justify-content: center;
    align-items: center;
    
}
.submit-button{
    display: flex;

    /*width: 90px;
    /*justify-content: center;*/
}

</style>