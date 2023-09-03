<template>
  <div class="total-layout">
    <div class="tab">
      <el-radio-group v-model="label_default" size="large">
        <el-radio-button @click="selectTab(0)" label="ID登录"/>
        <el-radio-button @click="selectTab(1)" label="手机号码登录"/>
      <!--<el-radio-button v-for="(tab, index) in tabs" :key="index" @click="selectTab(index)">
      {{ tab.name }}
      </el-radio-button>-->
    </el-radio-group>
    </div>
    <br>

    <div class="component">
      <component :is="selectedTab"></component>
    </div>
    <div class="register">
      <el-button
      type="button.type"
      @click="toRegister"
      >没有账号？立即注册</el-button>
    </div>

    <div class="register">
      <el-button
      type="button.type"
      @click="toFindPassword"
      >找回密码</el-button>
    </div>
  <el-button type="primary" @click="router.push('/administrator')">管理员登录</el-button>
  </div>
</template>

<script lang="ts" setup>

import {ref,shallowRef} from 'vue';
import { useRouter } from 'vue-router';
import IDlogin from '../components/IDLoginPage.vue'
import phonelogin from '../components/PhoneLoginPage.vue'


  const router=useRouter()
  const label_default=ref('ID登录')
  const tabs=[
        { name: 'ID登录', component: IDlogin },
        { name: '电话号码登录', component: phonelogin },
  ]
  const selectedTab=shallowRef(IDlogin)
  
  const selectTab=(index:number)=> {
      console.log(index)
      selectedTab.value = tabs[index].component;
      console.log(selectedTab.value)
    }
  
  const toRegister=()=>{
      //router.push({name:'register'})
      /*-------------------- */
      /*跳转到注册界面修改此处 */
      router.push({name: 'userregister'}); // 跳转到注册界面
      /*-------------------- */
  }

  const toFindPassword=()=>{
      router.push({name:'findpassword'})
  }
</script>

<style scoped>

.total-layout {

  position: relative;
  top: 130px;
  justify-content: center;
}

.tab {
  display: flex;
  position: relative;
  top: 30%;
  justify-content: center;
}

.tab-content {
  display: flex;
  
  justify-content: center;
}

.component {
  display: flex;
  flex-direction:column;
  justify-content: center;
}

.register{
  display: flex;
  margin-top: 20px;
  justify-content: center;
}
</style>