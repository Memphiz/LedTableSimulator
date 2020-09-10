#!/bin/sh

if [ $# != 1 ]
then
  echo "usage: $0 <mqttserver ip>"
  exit 1
fi

ANIM_LOOPS=5
NUM_FRAMES=4

while [ $ANIM_LOOPS -gt 0 ]
do
  for i in frameanim*.*
  do
    echo "mosquitto_pub -h $1 -t \"ledtable/simulation/framebuffer\" -f ./$i"
    mosquitto_pub -h $1 -t "ledtable/simulation/framebuffer" -f ./$i
    sleep 0.1
  done
  ANIM_LOOPS=`expr $ANIM_LOOPS - 1`
done

echo "mosquitto_pub -h $1 -t \"ledtable/simulation/framebuffer\" -f ./colorwheel.frame"
mosquitto_pub -h $1 -t "ledtable/simulation/framebuffer" -f ./colorwheel.frame
sleep 0.1
