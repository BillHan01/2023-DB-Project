<template>
    <div ref="chartDom" style="width: 100%; height: 400px;"></div>
  </template>
  
  <script lang="ts">
  import { ref, shallowRef, onMounted, watch } from 'vue'
  import * as echarts from 'echarts/core'
  import {
    LineChart
  } from 'echarts/charts'
  import {
    GridComponent,
    TooltipComponent,
    ToolboxComponent
  } from 'echarts/components'
  import {
    CanvasRenderer
  } from 'echarts/renderers'
  
  echarts.use(
    [LineChart, GridComponent, TooltipComponent, ToolboxComponent, CanvasRenderer]
  )
  
  export default {
    props: {
      data: {
        type: Array,
        required: true
      }
    },
    setup(props) {
      const chartDom = ref(null)
      const chartInstance = shallowRef<echarts.ECharts | null>(null)
  
      onMounted(() => {
        if (chartDom.value) {
          chartInstance.value = echarts.init(chartDom.value)
          const option = {
            xAxis: {
              type: 'category',
              data: props.data.map(item => item.com_pc_time)
            },
            yAxis: {
              type: 'value'
            },
            series: [{
              data: props.data.map(item => item.com_pc_price),
              type: 'line'
            }]
          }
          if (chartInstance.value) {
            chartInstance.value.setOption(option)
          }
        }
      })
  
      watch(props.data, (newData) => {
        if (chartInstance.value) {
          const option = {
            xAxis: {
              data: newData.map(item => item.com_pc_time)
            },
            series: [{
              data: newData.map(item => item.com_pc_price)
            }]
          }
          chartInstance.value.setOption(option)
        }
      })
  
      return {
        chartDom
      }
    }
  }
  </script>
  