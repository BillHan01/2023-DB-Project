<template>
    <div>
    <h1 class="header">通过电话号码登录</h1>
    <div>
    <el-form label-width="100px" style="max-width: 460px;position: relative;margin: auto;">
      <el-form-item label="电话号码" :validate-status="phoneStatus" :error="phoneError">
        <el-input type="tel" v-model="loginPhoneNumber" placeholder="请输手机号" @blur="validatePhone" required/>
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
import {idState } from '../../router/userID.js'

  const phoneStatus=ref('')
  const phoneError=ref('')
  const loginPhoneNumber=ref('')
  const loginPassword=ref('')
  const router = useRouter();
  const phoneReg = /^1[3-9]\d{9}$/;
  //const router=useRouter()
  /*地址修改此处*/ 
  const address='http://localhost:5000/api/'
  /*           */

const validatePhone = () => {
  if (!phoneReg.test(loginPhoneNumber.value)) {
    phoneStatus.value = 'error';
    phoneError.value = '电话号码格式不正确';
  } else {
    phoneStatus.value = 'success';
    phoneError.value = '';
  }
};

const login_id=async ()=> {
          axios.post(address+'login',  JSON.stringify({ 
            user_ID_phonenumber:loginPhoneNumber.value,
            user_password:loginPassword.value,
            login_type:"1" 
          }), {
          headers: {
              'Content-Type': 'application/json'
          }
          })
          .then(response => {
              console.log('Login submitted successfully.');
              console.log(response.data);
              if(response.data.message==='success'){
                /*------------------------*/
                /*登录成功后编辑此处跳转界面*/
                /*------------------------*/
                localStorage.setItem("user_id",response.data.user_ID );
                if (response.data.user_type=='1'){
                  sessionStorage.removeItem("sto_id");
                  sessionStorage.removeItem("cus_id");
                  sessionStorage.removeItem("user_type");
                  sessionStorage.setItem("sto_id", response.data.user_ID);
                  sessionStorage.setItem("user_type", response.data.user_type);
                    router.push({
                      path:'/store',
                  });
                }
                else if (response.data.user_type=='0'){
                  sessionStorage.removeItem("sto_id");
                  sessionStorage.removeItem("cus_id");
                  sessionStorage.removeItem("user_type");
                  sessionStorage.setItem("cus_id", response.data.user_ID);
                  sessionStorage.setItem("user_type", response.data.user_type);
                  console.log("loginUsername.value:"+response.data.user_ID)
                    router.push({
                      path:'/home',
                  });
                }
                //router.push({name:'MiddleTest',query:{id:response.data.user_ID}})
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

.form-layout{
    display: relative;
    margin-top: 30px;
}
.header{
    display: flex;
    flex-direction:column;
    justify-content: center;
}
.submit-button{
    display: flex;
    margin-top: 30px;
    flex-direction:column;
    justify-content: center;
}
</style>