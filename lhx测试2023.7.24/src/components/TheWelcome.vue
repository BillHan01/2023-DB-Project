<template>
  <div>
    
    <!-- <form @submit.prevent="login">
      <div>
        <label for="username">Username:</label>
        <input type="text" id="username" v-model="loginUsername" required>
      </div>
      <div>
        <label for="password">Password:</label>
        <input type="password" id="password" v-model="loginPassword" required>
      </div>
      <div>
        <button type="submit">Login</button>
      </div>
    </form> -->
    <el-container>
      <el-header height="200px"></el-header>
    <el-container>
      <el-aside width="300px"></el-aside>
      
      <el-main>
      <h1>Login</h1>
      <el-form :model="loginForm"
      label-position="Right"
      label-width="100px"
      ref="formRef"
     >
        <el-form-item label="User name"
        :rules="[
            { required: true, message: 'name is required' }
          ]"
          prop="name">
          <el-input v-model="loginForm.name" placeholder="Please input username"   type="text"
           />
        </el-form-item>
        <el-form-item label="Password"
        :rules="[
            { required: true, message: 'password is required' }
          ]"
           prop="password">
          <el-input v-model="loginForm.password"  type="password" 
          placeholder="Please input password" show-password  
           />
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="login(formRef)">Login</el-button>
        </el-form-item>
      </el-form>
      </el-main>
      <el-aside width="300px"></el-aside>
    </el-container>
    </el-container>
    <!-- <h1>Register</h1>
    <form @submit.prevent="register">
      <div>
        <label for="register-username">Username:</label>
        <input type="text" id="register-username" v-model="registerUsername" required>
      </div>
      <div>
        <label for="register-password">Password:</label>
        <input type="password" id="register-password" v-model="registerPassword" required>
      </div>
      <div>
        <label for="register-usertype">usertype:(0为用户，1为商家，其他报错)</label>
        <input type="text" id="register-usertype" v-model="usertype" required>
      </div>
      <div>
        <button type="submit">Register</button>
      </div>
    </form> -->
  </div>

 
</template>

<script lang="ts" setup >
import axios from 'axios';
import {useRouter} from 'vue-router'
import {reactive, ref} from 'vue'
import type { FormInstance } from 'element-plus'
//const loginUsername=ref("")
// const loginPassword=ref("")
// const imageUrl=ref("")
const usertype=ref(null)
// const registerUsername=ref("")
// const registerPassword=ref("")
const user_id=ref("")
const formRef = ref<FormInstance>()
const loginForm=reactive({
  name:"",
  password:""
})

const router =useRouter()


function  login(formEl: FormInstance | undefined) {
        if (!formEl) return
        formEl.validate((valid) => {
          if (valid) {
            console.log('submit!',loginForm.name,loginForm.password)
            axios.post(
              //'/api/login',  
              'http://localhost:5000/api/login', 
          JSON.stringify({ username:loginForm.name,password:loginForm.password }), 
          {
            headers: {
                'Content-Type': 'application/json'
            }
          })
          .then((res) => {
              usertype.value=res.data.usertype
              console.log('Login submitted successfully.',res.data);
              if (usertype.value==0){
                  user_id.value=res.data.userid
                  router.push({
                  name:'commodity',
                  query: {
                    username:loginForm.name,
                    user_id:user_id.value
                  }
                })
              }
              else if(usertype.value==1){
                user_id.value=res.data.userid
                router.push({
                  name:'commodityupload',
                  query: {
                    username:loginForm.name,
                    user_id:user_id.value
                  }
                })
              }
          })
          .catch((error) => {
              console.log('An error occurred:', error);
          });
          } else {
            console.log('error submit!')
            return false
          }
        })
      }
// function  register() {
//           axios.post('/api/register',  JSON.stringify({ username:this.registerUsername,password:this.registerPassword ,usertype:this.usertype}), {
//           headers: {
//               'Content-Type': 'application/json'
//           }
//           })
//           .then(() => {
//               console.log('Register submitted successfully.');
//           })
//           .catch((error) => {
//               console.log('An error occurred:', error);
//           });
//       }

// export default {
//   data() {
//     return {
//       loginUsername:'',
//       loginPassword:'',
//       imageUrl: '',
//       usertype:'',
//       registerUsername:'',
//       registerPassword:'',
//       user_id:null
//     };
//   },
//   methods: {
//       getPicture(){
//         axios.get('/api/picture/1', { responseType: 'blob' })
//         .then((response) => {
//           const imageUrl = URL.createObjectURL(response.data);
//           console.log(imageUrl);
//           this.imageUrl = imageUrl; // 将获取到的图片URL设置给data属性
//         })
//         .catch((error) => {
//           console.log('An error occurred:', error);
//         });
//       },

//       login() {
//           axios.post('/api/login',  
//           JSON.stringify({ username:this.loginUsername,password:this.loginPassword }), 
//           {
//             headers: {
//                 'Content-Type': 'application/json'
//             }
//           })
//           .then((res) => {
              
//               this.usertype=res.data.usertype
//               console.log('Login submitted successfully.',res.data);
//               if (this.usertype==0){
//                 this.user_id=res.data.userid
//                 this.$router.push({
//                   name:'commodity',
//                   query: {
//                     username:this.loginUsername,
//                     user_id:this.user_id
//                   }
//                 })
//               }
//               else if(this.usertype==1){
//                 this.user_id=res.data.userid
//                 this.$router.push({
//                   name:'commodityupload',
//                   query: {
//                     username:this.loginUsername,
//                     user_id:this.user_id
//                   }
//                 })
//               }
//           })
//           .catch((error) => {
//               console.log('An error occurred:', error);
//           });
//       },
//       register() {
//           axios.post('/api/register',  JSON.stringify({ username:this.registerUsername,password:this.registerPassword ,usertype:this.usertype}), {
//           headers: {
//               'Content-Type': 'application/json'
//           }
//           })
//           .then(() => {
//               console.log('Register submitted successfully.');
//           })
//           .catch((error) => {
//               console.log('An error occurred:', error);
//           });
//       }
//   }
// }
</script>