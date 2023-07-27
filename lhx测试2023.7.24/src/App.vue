<template>
  <router-view></router-view>
</template>

<script lang="ts" setup>
import axios from 'axios';
import {useRouter} from 'vue-router'
import {onBeforeMount} from 'vue'
//axios.defaults.baseURL = 'http://124.70.7.210:5000';//网址基址
//axios.defaults.baseURL = 'http://localhost:5000';//网址基址
const router=useRouter()
onBeforeMount(()=>{
  router.push('/search');
})

</script>
