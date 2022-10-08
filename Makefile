

software:
	nohup ./RelatedSoftwares/Consul/consul.exe agent -dev >./RelatedSoftwares/Consul/consul.log &2>1 &
	
clean:
	pkill -9 consul