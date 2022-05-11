
docker-compose -f docker-compose-localstack.yml up

 
aws --endpoint-url=http://localhost:4566 cloudformation list-stacks
 
aws --endpoint-url=http://localhost:4566 sns list-topics
  
aws --endpoint-url=http://localhost:4566 sqs list-queues


aws --endpoint-url=http://localhost:4566 cloudformation deploy --template-file queue.yml --stack-name queue-UAT --parameter-overrides EnvironmentType=UAT --region=us-east-1
 
aws --endpoint-url=http://localhost:4566 cloudformation list-stacks
 
aws --endpoint-url=http://localhost:4566 sns list-topics
  
aws --endpoint-url=http://localhost:4566 sqs list-queues
   
aws --endpoint-url=http://localhost:4566 sns list-subscriptions

 aws --endpoint-url=http://localhost:4566 sqs receive-message --queue-url=http://localhost:4566/000000000000/order-request-queue-UAT.fifo
 
 aws --endpoint-url=http://localhost:4566 sns publish --topic-arn "arn:aws:sns:us-east-1:000000000000:order-requests-topic-UAT.fifo" --message file://C:\order-requests\order-request.txt --region=us-east-1
 
 aws --endpoint-url=http://localhost:4566 sqs receive-message --queue-url=http://localhost:4566/000000000000/order-request-queue-UAT.fifo
 
 aws --endpoint-url=http://localhost:4566 sqs purge-queue --queue-url=http://localhost:4566/000000000000/order-request-queue-UAT.fifo
 
  aws --endpoint-url=http://localhost:4566 cloudformation delete-stack --stack-name provisioning-engine-queue-UAT --region=us-east-1
