1. 通用服启动logstash
2. 每台游戏服启动一个filebeat, 连接logstash

 	配置 filebeat.yml

- type: filestream

enabled: true

path:

- /data/zones/dev*/datalog/*

output.logstash:

  # The Logstash hosts

  hosts: ["localhost:5044"]



安装logstash+elasticsearch+kibana

[日志服](https://snh48group.yuque.com/lw0nsy/zeet2g/sm6urwv1ait5aiyv)

